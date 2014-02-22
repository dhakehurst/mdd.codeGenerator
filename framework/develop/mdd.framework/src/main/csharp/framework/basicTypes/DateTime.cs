/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.basicTypes 
{
	//Primitive Type
	public class DateTime
	{
	
	  // --- Constructors ---
	    public DateTime(System.DateTime value) {this.value = value;}
	    public DateTime(DateTime value) {this.value = value.value;}
	
        System.DateTime value;

	  // --- Operations ---
		public global::framework.basicTypes.DateTime now() 
        {
		  return new DateTime(System.DateTime.Now);
		}


        public System.DateTime to_Date_Time() { return value; }

        public override System.String ToString() {
            return this.value.ToString("YYYY-MMM-dd");
        }
	}

}
