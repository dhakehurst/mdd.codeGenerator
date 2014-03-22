/************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
package mdd.codeGenerator.common.mojo;

import java.io.File;
import java.io.IOException;
import java.net.URL;
import java.net.URLClassLoader;
import java.security.CodeSource;
import java.util.ArrayList;
import java.util.List;

import org.eclipse.acceleo.common.IAcceleoConstants;
import org.eclipse.acceleo.engine.service.AbstractAcceleoGenerator;
import org.eclipse.acceleo.model.mtl.MtlPackage;
import org.eclipse.acceleo.model.mtl.resource.EMtlResourceFactoryImpl;
import org.eclipse.emf.common.util.Monitor;
import org.eclipse.emf.common.util.URI;
import org.eclipse.emf.ecore.EObject;
import org.eclipse.emf.ecore.EPackage;
import org.eclipse.emf.ecore.resource.Resource;
import org.eclipse.emf.ecore.resource.ResourceSet;
import org.eclipse.emf.ecore.resource.URIConverter;
import org.eclipse.emf.ecore.resource.impl.ExtensibleURIConverterImpl;
import org.eclipse.emf.ecore.xmi.impl.EcoreResourceFactoryImpl;
import org.eclipse.uml2.uml.UMLPackage;
import org.eclipse.uml2.uml.resource.UMLResource;

public class Generate extends AbstractAcceleoGenerator {
    
	String _moduleName;
    @Override
    public String getModuleName() {
        return _moduleName;
    }
    
    String[] _templateNames;
    @Override
    public String[] getTemplateNames() {
        return _templateNames;
    }

    List<String> propertyFiles = new ArrayList<String>();
    @Override
    public List<String> getProperties() {
    	return propertyFiles;
    }
    @Override
	public void addPropertiesFile(String propertiesFile) {
		this.propertyFiles.add(propertiesFile);
	}
    
    URLClassLoader _urlClassLoader;
    URLClassLoader getURLClassLoader() {
    	return _urlClassLoader;
    }
    
    public Generate(
    		String moduleName,
    		String[] templateNames,
    		URLClassLoader urlClassLoader,
    		URI modelURI,
    		File targetFolder,
    		List<? extends Object> arguments
    ) throws IOException {
    	this._moduleName = moduleName;
    	this._templateNames = templateNames;
    	this._urlClassLoader = urlClassLoader;
        initialize(modelURI, targetFolder, arguments);
    }

    @Override
    public void doGenerate(Monitor monitor) throws IOException {
    	System.out.println("Using property file: "+this.getProperties());
    	System.out.println("doGenerate");
        super.doGenerate(monitor);
    }

    @Override
	protected URIConverter createURIConverter() {
		return new ExtensibleURIConverterImpl() {
			/**
			 * {@inheritDoc}
			 * 
			 * @see org.eclipse.emf.ecore.resource.impl.ExtensibleURIConverterImpl#normalize(org.eclipse.emf.common.util.URI)
			 */
			@Override
			public URI normalize(URI uri) {
				//System.out.println("*** uri: "+uri);
				URI normalized = getURIMap().get(uri);
				if (normalized == null) {
					//org.eclipse.acceleo.maven.AcceleoURIHandler resolver = new AcceleoURIHandler();

					if (uri != null) {
						URI reusableURI = uri;
						//System.out.println("*** reusableURI: "+reusableURI);
						if (-1 < reusableURI.toString().indexOf("platform:/plugin/")) {
							//System.out.println("*** uri: "+uri);
							//resolve platform: to jar
							String jarFileName = reusableURI.toString().substring("platform:/plugin/".length());
							String fileName = jarFileName.substring(jarFileName.indexOf("/")+1);
							//System.out.println("*** fileName: "+fileName);
							URL url = getURLClassLoader().getResource(fileName);
							//System.out.println("*** url: "+url);
							URI nn = URI.createURI(url.toString());
							//System.out.println("*** nn: "+nn);
							getURIMap().put(uri, nn);
							return nn;
							//getLog().debug("transform uri for "+resourceURI+" -> "+reusableURI);
						} else {
							//getLog().debug("Failed transform uri for "+resourceURI);
						}
					}
				} else {
					//System.out.println("*** normalized: "+normalized);
					return normalized;
				}
				return super.normalize(uri);
			}
		};

	}
    
    @Override
    protected URL findModuleURL(String moduleName) {
    	//System.out.println("*** moduleName: "+moduleName);
    	URL url = super.findModuleURL(moduleName);
    	//System.out.println("*** url: "+url);
    	return url;
    }
    
    public ResourceSet resourceSet;
    

    @Override
    public void registerResourceFactories(ResourceSet resourceSet) {
        super.registerResourceFactories(resourceSet);
        this.resourceSet = resourceSet;
        
        Resource.Factory.Registry.INSTANCE.getExtensionToFactoryMap().put("ecore", new EcoreResourceFactoryImpl());
        Resource.Factory.Registry.INSTANCE.getExtensionToFactoryMap().put(IAcceleoConstants.EMTL_FILE_EXTENSION, new EMtlResourceFactoryImpl());

        Resource.Factory.Registry.INSTANCE.getExtensionToFactoryMap().put(UMLResource.FILE_EXTENSION, UMLResource.Factory.INSTANCE);
		Resource.Factory.Registry.INSTANCE.getExtensionToFactoryMap().put("java", new EMtlResourceFactoryImpl());

	    EPackage.Registry.INSTANCE.put(UMLPackage.eNS_URI, UMLPackage.eINSTANCE);
	    EPackage.Registry.INSTANCE.put("http://www.eclipse.org/uml2/2.0.0/UML", UMLPackage.eINSTANCE);
		
	    CodeSource umlModel = UMLResource.class.getProtectionDomain().getCodeSource();
	    if (null != umlModel) {
			String libraryLocation = umlModel.getLocation().toString();
			libraryLocation = libraryLocation.replace(
					"org.eclipse.uml2.uml/3.2.100/org.eclipse.uml2.uml-3.2.100",
					"org.eclipse.uml2.uml.resources/3.1.100/org.eclipse.uml2.uml.resources-3.1.100"
			);
			if (libraryLocation.endsWith(".jar")) {
				libraryLocation = "jar:" + libraryLocation + "!/";
			}
			URI uri = URI.createURI(libraryLocation);
			URIConverter.URI_MAP.put(URI.createURI(UMLResource.LIBRARIES_PATHMAP), uri.appendSegment("libraries").appendSegment(""));
			URIConverter.URI_MAP.put(URI.createURI(UMLResource.METAMODELS_PATHMAP), uri.appendSegment("metamodels").appendSegment(""));
			URIConverter.URI_MAP.put(URI.createURI(UMLResource.PROFILES_PATHMAP), uri.appendSegment("profiles").appendSegment(""));
	    } else {

	    }
		CodeSource acceleoModel = MtlPackage.class.getProtectionDomain().getCodeSource();
		if (acceleoModel != null) {
			String libraryLocation = acceleoModel.getLocation().toString();
			if (libraryLocation.endsWith(".jar")) {
				libraryLocation = "jar:" + libraryLocation + '!';
			}
			
			URIConverter.URI_MAP.put(URI.createURI("http://www.eclipse.org/acceleo/mtl/3.0/mtlstdlib.ecore"), URI.createURI(libraryLocation + "/model/mtlstdlib.ecore"));
			URIConverter.URI_MAP.put(URI.createURI("http://www.eclipse.org/acceleo/mtl/3.0/mtlnonstdlib.ecore"), URI.createURI(libraryLocation + "/model/mtlnonstdlib.ecore"));
		} else {

		}
	}
    
}
