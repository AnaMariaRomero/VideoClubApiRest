using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using VideoClubApiRest.Core.Entities;

namespace VideoClubApiRest.Infraestructure.Validators
{
    public class XMLValidators
    {
        private static string results = "";
        
        public static string ValidXML(Rents rent)
        {
            
            //load xml file with rent class

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(rent.GetType());
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                serializer.Serialize(ms, rent);
                ms.Position = 0;
                xmlDoc.Load(ms);

                File.WriteAllText(Path.GetFullPath("../VideoClubApiRest.Infraestructure/Validators/XMLFile1.xml"), xmlDoc.InnerXml);
            }

            //Load the XmlSchemaSet.
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add("", Path.GetFullPath("../VideoClubApiRest.Infraestructure/Validators/RentsXMLSchema.xsd"));

            //Validate the file using the schema stored in the schema set.
            //Any elements belonging to the namespace "urn:cd-schema" generate
            //a warning because there is no schema matching that namespace.
            Validate(Path.GetFullPath("../VideoClubApiRest.Infraestructure/Validators/XMLFile1.xml"), schemaSet);

            return results;
        }
        private static void Validate(String filename, XmlSchemaSet schemaSet)
        {
            XmlSchema compiledSchema = null;

            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                compiledSchema = schema;
            }
            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            settings.Schemas.Add(compiledSchema);
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            settings.ValidationType = ValidationType.Schema;

            //Create the schema validating reader.
            XmlReader vreader = XmlReader.Create(filename, settings);

            while (vreader.Read())
            { }
                //Close the reader.
                vreader.Close();
            
        }
        //Display any warnings or errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
            {
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
                results += args.Message;
            }
            else
            {
                Console.WriteLine("\tValidation error: " + args.Message);
                results += args.Message;
            }

        }
    }
}