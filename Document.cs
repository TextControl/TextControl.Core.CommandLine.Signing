using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXTextControl;

namespace tx_sign_console {
	public class Document {
		public static void SignDocument() {

			using (ServerTextControl tx = new ServerTextControl()) {

				tx.Create();

				// add dummy text
				tx.Text = "This is a signed document";

				SaveSettings saveSettings = new SaveSettings();

				// add digital signature to sign complete document
				saveSettings.DigitalSignature = new DigitalSignature(
					new System.Security.Cryptography.X509Certificates.X509Certificate2("textcontrolself.pfx", "123"), null);

				// export to PDF
				tx.Save("signed_documents.pdf", StreamType.AdobePDF, saveSettings);
			}

		}

		public static void SignFields() {
			using (ServerTextControl tx = new ServerTextControl()) {

				tx.Create();

				// create a signature field
				SignatureField signatureField = new SignatureField(
					new System.Drawing.Size(2000, 2000), "txsign", 10);

				// set image representation
				signatureField.Image = new SignatureImage("signature.svg", 0);

				// insert the field
				tx.SignatureFields.Add(signatureField, -1);

				// create a digital signature (for each field, if required)
				DigitalSignature digitalSignature = new DigitalSignature(
					new System.Security.Cryptography.X509Certificates.X509Certificate2(
						"textcontrolself.pfx", "123"), null, "txsign");

				// apply the signatures to the SaveSettings
				SaveSettings saveSettings = new SaveSettings() {
					SignatureFields = new DigitalSignature[] { digitalSignature }
				};

				// export to PDF
				tx.Save("signed_fields.pdf", StreamType.AdobePDF, saveSettings);
			}
		}
	}
}
