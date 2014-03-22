/************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
package mdd.codeGenerator.csharp.mojo;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLClassLoader;
import java.security.CodeSource;
import java.util.ArrayList;
import java.util.List;

import mdd.codeGenerator.common.mojo.Generate;

import org.apache.maven.plugin.AbstractMojo;
import org.apache.maven.plugin.MojoExecutionException;
import org.apache.maven.plugin.MojoFailureException;
import org.apache.maven.project.MavenProject;
import org.apache.maven.artifact.DependencyResolutionRequiredException;

import org.eclipse.acceleo.common.IAcceleoConstants;
import org.eclipse.acceleo.engine.service.AbstractAcceleoGenerator;
import org.eclipse.acceleo.internal.parser.compiler.IAcceleoParserURIHandler;
import org.eclipse.acceleo.model.mtl.MtlPackage;
import org.eclipse.acceleo.model.mtl.resource.EMtlResourceFactoryImpl;
import org.eclipse.emf.common.util.BasicMonitor;
import org.eclipse.emf.common.util.URI;
import org.eclipse.emf.ecore.EPackage;
import org.eclipse.emf.ecore.resource.Resource;
import org.eclipse.emf.ecore.resource.ResourceSet;
import org.eclipse.emf.ecore.resource.URIConverter;
import org.eclipse.emf.ecore.xmi.impl.EcoreResourceFactoryImpl;
import org.eclipse.uml2.uml.UMLPackage;
import org.eclipse.uml2.uml.resource.UMLResource;

/**
 * @goal csharp
 * @phase generate-sources
 * @requiresDependencyResolution runtime
 */
public class MavenPlugin extends AbstractMojo {

	/**
	 * @parameter default-value="${project}"
	 * @required
	 * @readonly
	 */
	MavenProject project;

	/**
	* The class to use for the uri converter.
	*
	* @parameter expression = "${acceleo-compile.uriHandler}"
	*/
	private String uriHandler;
	
	/**
	* The path to the mtl file where the templates to invoke are defined.
	*
	* @parameter expression = "${acceleo-compile.uriHandler}"
	*/
	private String acceleoModuleFile;
	
	/**
	* The comma separated list of template names to invoke.
	*
	* @parameter expression = "${acceleo-compile.uriHandler}"
	*/
	private String acceleoTemplates;
    /**
     * @parameter
     * (this must exist for maven to set the property.)
     */
    private List<String> models;

    /**
     * List of Artifacts to generate
     * Artifact {
     *   name : String,
     *   pom : Pom {
     *     generate : Boolean,
     *     groupId : String,
     *     packaging : String
     *   }
     * }
     * 
     * @parameter
     * (this must exist for maven to set the property.)
     */
    private List<Artifact> artifacts;
	
    /**
     * @parameter
     * (this must exist for maven to set the property.)
     */
    private String targetFolder;


	@Override
	public void execute() throws MojoExecutionException, MojoFailureException {
        try {
		
            if (models.isEmpty() || null == targetFolder) {
                getLog().error("Properties not set : {models:[modelUri], targetFolder:Path}.");
            } else {
            	for (String modelUri : this.models) {
	            	File model = new File(modelUri);
	                URI modelURI = URI.createFileURI(model.getAbsolutePath());
	                File folder = new File(targetFolder);
	                getLog().info("model = "+modelURI.toString());
	                getLog().info("acceleoModuleFile = "+acceleoModuleFile);
	                getLog().info("acceleoTemplates = "+acceleoTemplates);

	                
	                if (this.artifacts.isEmpty()) {
		                getLog().info("Generating all Artifacts");	                	
	                	//artifacts not defined, so generate all
		                /*
		                 * Add in this list all the arguments used by the starting point of the generation
		                 * If your main template is called on an element of your model and a String, you can
		                 * add in "arguments" this "String" attribute.
		                 */
		                List<String> arguments = new ArrayList<String>();
		                arguments.add(this.targetFolder);
		                arguments.add(null);
		                
		                Generate generator = new Generate(
		                		this.acceleoModuleFile,
		                		this.acceleoTemplates.split(","),
		                		getURLClassLoader(),
		                		modelURI,
		                		folder,
		                		arguments
		                );		    			//plugInUriResolver(generator);
		                generator.doGenerate(null);	                	
	                } else {
	                	//generate each artifact separately
		                for (Artifact artifact : artifacts) {
			                getLog().info("artifact = "+artifact.name);
			                
			                //File propertyFile = new File(this.project.getBuild().getDirectory()+"/"+artifact.name+".properties");
			                File buildDir = new File(this.project.getBuild().getDirectory());
			                if (!buildDir.exists()) {
			                	buildDir.mkdir();
			                }
			                String propFileName = this.project.getBuild().getDirectory()+"/"+artifact.name+".properties";
			                File propertyFile = new File(propFileName);
			                propertyFile.createNewFile();
			                getLog().info("Creating property file: "+propertyFile.getAbsolutePath());
			                
			                String eol = System.getProperty("line.separator");
			                String[] props = artifact.properties.split("\\r?\\n");
			                
			                FileWriter w = new FileWriter(propertyFile);
			                for(String p : props) {
			                	w.write(p.trim()+eol);
			                }
			                
			                w.flush();
			                
			                getLog().info("properties: "+artifact.properties);
			                
			                List<String> arguments = new ArrayList<String>();
			                arguments.add(artifact.name);
			                
			                Generate generator = new Generate(
			                		this.acceleoModuleFile,
			                		this.acceleoTemplates.split(","),
			                		getURLClassLoader(),
			                		modelURI,
			                		folder,
			                		arguments
			                );
			    			//plugInUriResolver(generator);			                
			                generator.getProperties().add(propFileName);
			                
			                generator.doGenerate(null);							
						}
	                }
				}
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
	}

	URLClassLoader getURLClassLoader() {

		URLClassLoader newLoader = null;
		try {
			List<?> runtimeClasspathElements = project.getRuntimeClasspathElements();
			List<?> compileClasspathElements = project.getCompileClasspathElements();
			URL[] runtimeUrls = new URL[runtimeClasspathElements.size() + compileClasspathElements.size()];
			int i = 0;
			for (Object object : runtimeClasspathElements) {
				if (object instanceof String) {
					String str = (String) object;
					getLog().debug("Adding the runtime dependency " + str + " to the classloader for the package resolution");
					runtimeUrls[i] = new File(str).toURI().toURL();
					i++;
				} else {
					getLog().debug("Runtime classpath entry is not a string: " + object);
				}
			}
			for (Object object : compileClasspathElements) {
				if (object instanceof String) {
					String str = (String) object;
					getLog().debug("Adding the compilation dependency " + str + " to the classloader for the package resolution");
					runtimeUrls[i] = new File(str).toURI().toURL();
					i++;
				} else {
					getLog().debug("Runtime classpath entry is not a string: " + object);
				}
			}
			newLoader = new URLClassLoader(runtimeUrls, Thread.currentThread().getContextClassLoader());
			return newLoader;
		} catch (DependencyResolutionRequiredException e) {
			getLog().error(e);
		} catch (MalformedURLException e) {
			getLog().error(e);
		}
		return null;
	}

}
