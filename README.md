# dotnet-plist

A .Net Library for generating PList XML files

## Why

I'm a .Net developer and whilst developing a sample MDM (Mobile Device Management) solution for testing my Roomr app (www.roomrapp.com) I needed to create PList files to configure my iOS 7 device. I couldn't find a nice library for working with PList XML, so I created one.

## Requirements

The solution requires .Net 4.5 and Visual Studio 2012 or higher.

## Technical Stuff

To generate a PList file, start by instantiating a new PListGenerator.

    PListGenerator generator = PListGenerator.New();

The API then takes on a more fluid approach (I hope you think so at least!)

To add a dictionary, you use 

	PListDictionary dictionary = generator.AddDictionary();

or you can add it as a key

	PListDictionary dictionary = generator.AddDictionary("ConsentText");

You can then add the various property values such as string, integer, bool etc.

	dictionary.AddString("PayloadDescription", "Adds this device to the Family MDM");
    dictionary.AddString("PayloadDisplayName", "Family MDM");
    dictionary.AddString("PayloadIdentifier", "com.coldbear.mdm");
    dictionary.AddString("PayloadOrganization", "Cold Bear");
    dictionary.AddBool("PayloadRemovalDisallowed");
    dictionary.AddString("PayloadUUID", PListGenerator.NewUUID());
    dictionary.AddInteger("PayloadVersion", 1);

You can also string the commands together e.g. create a dictionary and add a key/value to it.

	PListDictionary dictionary = generator.AddDictionary("ConsentText").AddString("key", "value");

Arrays are added in the same way

 	PListArray array = generator.AddArray("PayloadContent");

Once you're finished populating the PList, just call GetXML() on the generator to return a byte[] containing the XML.

	byte[] xmlBody = generator.GetXml();

You can also return a string

	string xmlString = generator.GetString();

To create UUIDs, there is a convenience method on PListGenerator

	string UUID = PListGenerator.NewUUID()

## Future Development

This library is in its infancy and there is a lot of work left to do on it. I'll be updating it over time as my needs dictate. If you want to help, please contribute.

## Contribute

All pull requests are welcomed! If you come across an issue you cannot fix, please raise an issue or drop me an email at tomas@tomasmcguinness.com or follow me on twitter @tomasmcguinness

## License

dotnet-plist is distributed under the MIT license: [http://tomasmcguinness.mit-license.org/](http://tomasmcguinness.mit-license.org/)
