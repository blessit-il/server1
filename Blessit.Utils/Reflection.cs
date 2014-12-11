using System;
using System.Reflection;
using System.Xml;

namespace MindGame.Utils
{
	/// <summary>
	/// Summary description for Reflection.
	/// </summary>
	public class Reflection
	{
		#region Fields
		/// <summary>
		/// Stores the value for property Object.
		/// </summary>
		private object obj;
		/// <summary>
		/// Stores the value for property PropertyName.
		/// </summary>
		private string propertyName;
		/// <summary>
		/// Stores the value for property PropertyValue.
		/// </summary>
		private object propertyValue;
		/// <summary>
		/// Stores the value for property PropertyType.
		/// </summary>
		private Type propertyType;
		/// <summary>
		/// Stores the value for property PropertyInfo.
		/// </summary>
		private PropertyInfo propertyInfo;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		public Reflection()
		{
		}
		#endregion

		#region Properties
		/// <summary>
		/// Get/Set the object.
		/// </summary>
		public object Object
		{
			get{return obj;}
			set{obj = value;}
		}

		/// <summary>
		/// Get/Set the name of property.
		/// </summary>
		public string PropertyName
		{
			get{return propertyName;}
			set{propertyName = value;}
		}

		/// <summary>
		/// Get/Set the value of property.
		/// </summary>
		public object PropertyValue
		{
			get
			{
				//if (propertyValue == null)
					GetPropertyValue();
				return propertyValue;
			}
			set
			{
				SetPropertyValue(value);
			}
		}

		/// <summary>
		/// Data type for the specified property.
		/// </summary>
		public Type PropertyType
		{
			get
			{
				//if (propertyType == null)
					GetPropertyType();
				return propertyType;
			}
			set
			{
				propertyType = value;
			}
		}

		/// <summary>
		/// Get the property info object.
		/// </summary>
		private PropertyInfo PropertyInfo
		{
			get
			{
				//if (propertyInfo == null)
					propertyInfo = Object.GetType().GetProperty(PropertyName);
				return propertyInfo;
			}
		}

		/// <summary>
		/// Get if property exists in the object.
		/// </summary>
		public bool PropertyExists
		{
			get
			{
				return PropertyInfo != null;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Searches the property inside the object
		/// and gets the type for the property.
		/// </summary>
		private Type GetPropertyType()
		{
			if (PropertyInfo != null)
			{
				propertyType = PropertyInfo.PropertyType;
			}
			return propertyType;
		}

		/// <summary>
		/// Searches the property inside the object
		/// and gets the value for the property.
		/// </summary>
		private void GetPropertyValue()
		{
			if (PropertyInfo != null)
			{
				propertyValue = PropertyInfo.GetValue(Object, null);
			}
		}

		/// <summary>
		/// Searches the property inside the object
		/// and sets the value for the property in the object.
		/// </summary>
		private void SetPropertyValue(object oValue)
		{
			if (PropertyInfo != null)
			{
				propertyValue = oValue;
				try
				{
					if (oValue != null)
					{
						PropertyInfo.SetValue(Object, Convert.ChangeType(oValue, PropertyType), null);
					}
					else
					{
						PropertyInfo.SetValue(Object, null, null);
					}
				}
				catch (FormatException)
				{
					// TODO : Add logic for handling case when value is not set.
					// eat exception
				}
				catch (InvalidCastException)
				{
				}
			}
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// Checks if the property exists in the specified object.
		/// </summary>
		/// <param name="Object"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public static bool Exists(object oObject, string sPropertyName)
		{
			Reflection reflection = new Reflection();
			reflection.Object = oObject;
			reflection.PropertyName = sPropertyName;
			return reflection.PropertyExists;
		}

		/// <summary>
		/// Searches the property inside the object
		/// and returns the value for the property.
		/// </summary>
		/// <param name="Object">Object to search for the property.</param>
		/// <param name="propertyName">Name of the property to be searched.</param>
		/// <returns>Value of the property in the object.</returns>
		public static object GetValue(object oObject, string sPropertyName)
		{
			Reflection reflection = new Reflection();
			reflection.Object = oObject;
			reflection.PropertyName = sPropertyName;
			return reflection.PropertyValue;
		}

		/// <summary>
		/// Searches the property inside the object
		/// and returns the type for the property.
		/// </summary>
		/// <param name="Object">Object to search for the property.</param>
		/// <param name="propertyName">Name of the property to be searched.</param>
		/// <returns>Type of the property in the object.</returns>
		public static Type GetType(object oObject, string sPropertyName)
		{
			Reflection reflection = new Reflection();
			reflection.Object = oObject;
			reflection.PropertyName = sPropertyName;
			return reflection.PropertyType;
		}

		/// <summary>
		/// Searches the property inside the object and sets
		/// the value for property.
		/// </summary>
		/// <param name="Object">Object to search for the property.</param>
		/// <param name="propertyName">Name of the property to be searched.</param>
		/// <param name="sValue">Value to be set for the property.</param>
		public static void SetValue(object oObject, string sPropertyName, object oValue)
		{
			Reflection reflection = new Reflection();
			reflection.Object = oObject;
			reflection.PropertyName = sPropertyName;
			reflection.PropertyValue = oValue;
		}

		/// <summary>
		/// Sets the value of object using xpath.
		/// </summary>
		/// <param name="oObject"></param>
		/// <param name="xPath"></param>
		/// <param name="oValue"></param>
		public static void  SetValue(object oObject, object xPath, object oValue)
		{
			// Get index to exclude last item.
			int index = xPath.ToString().LastIndexOf("\\");

			if (index != -1)
			{
				// Get the xpath for getting the object.
				string strxPath = xPath.ToString().Substring(0, index);

				// Get the object containing the property to set value.
				object lastObject = GetObject(oObject, strxPath);

				// Get the property name.
				string propertyName = xPath.ToString().Substring(index + 1);

				// Set the value.
				SetValue(lastObject, propertyName, null);
			}// if (index != -1)
			else
			{
				SetValue(oObject, xPath.ToString(), null);
			}// else
		}

		/// <summary>
		/// Finds the object using xPath inside the object.
		/// </summary>
		/// <param name="oObject">Object to be searched.</param>
		/// <param name="xPath">xPath for the object.</param>
		/// <returns>Value of the property in the object.</returns>
		public static object GetObject(object oObject, string xPathString)
		{
			// Get array for xPath.
			string [] path = xPathString.Split('\\');

			// Object to store value.
			object Object = oObject;
			object indexer = null;
			// Loop through the path.
			for (int countPath=0; countPath < path.Length && Object != null; countPath++)
			{
				// Not a dictionary element.
				if (!path[countPath].StartsWith("@"))
				{
					Object = Reflection.GetValue(Object, path[countPath]);
				}// if (!path[countPath].StartsWith("@"))
					// Dictionary element.
				else
				{
					if(path[countPath].StartsWith("@@"))//the @@ means that it is an enum indexer
					{
						indexer = GetEnum(path[countPath].Substring(2));
					}// if(path[countPath].StartsWith("@@"))
					else
					{
						indexer = path[countPath].Substring(1);
					}// else
                    
					MethodInfo mi = Object.GetType().GetMethod("get_Item", new System.Type[]{indexer.GetType()});

					if(mi != null)
					{
						Object = mi.Invoke(Object,new Object[]{indexer});
					}//if(mi != null)

				}// else

			}// for (int countPath=0; countPath < path.Length && Object != null; countPath++)

			return Object;
		}

		/// <summary>
		/// Get the type of object inside an object at location specified by xpath.
		/// </summary>
		/// <param name="oObject">Object</param>
		/// <param name="xPathString">XPath.</param>
		/// <returns></returns>
		public static Type GetObjectType(object oObject, string xPathString)
		{
			// Get the object.
			object Object = GetObject(oObject, xPathString);

			// Initialize type with default of string.
			Type type = string.Empty.GetType();

			if (Object != null)
			{
				type = Object.GetType();
			}// if (Object != null)

			return type;
		}

		/// <summary>
		/// Gets an Enum with the specified path
		/// </summary>
		/// <param name="enumPath"></param>
		/// <returns></returns>
		public static Enum GetEnum(string enumPath)
		{
			string type = enumPath.Substring(0,enumPath.LastIndexOf("."));
			string ev = enumPath.Substring(enumPath.LastIndexOf(".")+1);
			Type t = Type.GetType(type);
			Enum e = (Enum)Enum.Parse(t, ev, true);
			return e;
		}

        /// <summary>
        /// Gets XML of an object.
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            string rootNodeName = Object.GetType().Name.ToLower();
            PropertyInfo[] properties = Object.GetType().GetProperties();
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateNode(XmlNodeType.Element, rootNodeName, string.Empty);

            for (int count = 0; count < properties.Length; count++)
            {
                PropertyInfo property = properties[count];
                XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, property.Name.ToLower(), string.Empty);
                node.InnerText = property.GetValue(Object, null).ToString();
                rootNode.AppendChild(node);
            }

            xmlDoc.AppendChild(rootNode);

            return xmlDoc.OuterXml;
        }

        /// <summary>
        /// Use this method to get XML for a list.
        /// </summary>
        /// <returns></returns>
        public string GetListXml()
        {
            MethodInfo mi = Object.GetType().GetMethod("get_Item", new System.Type[]{typeof (Int32)});
            PropertyName = "Count";
            int itemCount = (int)PropertyValue;
            string xml = string.Empty;

            for (int count = 0; count < itemCount; count++)
            {
                if (mi != null)
                {
                    object obj = mi.Invoke(Object, new Object[] { count });

                    Reflection reflect = new Reflection();
                    reflect.Object = obj;
                    xml += reflect.GetXml();
                }//if(mi != null)

            }

            return "<list>" + xml + "</list>";
        }
        #endregion
    }
}
