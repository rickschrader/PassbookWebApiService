.NET Passbook Open-source library
https://github.com/tomasmcguinness/dotnet-passbook

Generate Apple iOS certificates using Windows
http://tomasmcguinness.com/2012/06/28/generating-an-apple-ios-certificate-using-windows/
Importing Apple Cert into Windows
http://tomasmcguinness.com/2012/10/22/importing-apple-certificate-to-windows/


Apple's guide to building first pass
https://developer.apple.com/library/ios/documentation/UserExperience/Conceptual/PassKit_PG/Chapters/YourFirst.html

The main component of creating Passbook passes is creating a file package in the correct format.  This is a POC of a Web Api RESTful service that can generate those file packages.  

Part of that format requires some components of the package to be signed with appropriate developer certificates from Apple. You do need an Apple iOS Developer account in order to create the necessary certificates and generate usable Passbook passes. The cost for that account is $99/year. Apple does have an approval process for iOS (iPhone/iPad) apps, but I am not aware of one for Passbook pass generation.