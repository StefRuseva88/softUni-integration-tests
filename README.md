
# SoftUni Exam Projects - RestSharp API Tests 
[![C#](https://img.shields.io/badge/Made%20with-C%23-239120.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-5C2D91.svg)](https://dotnet.microsoft.com/)
[![JWT](https://img.shields.io/badge/Authentication-JWT-000000.svg)](https://jwt.io/)
[![NUnit](https://img.shields.io/badge/tested%20with-NUnit-22B2B0.svg)](https://nunit.org/)

### This is a test project for **Back-End Test Automation** March 2024 Course @ SoftUni
---
## Project Description
This repository contains a series of test projects designed to practice and demonstrate skills in back-end test automation. The tests are written using RestSharp for API interactions and NUnit for testing framework.

## Projects Included
- **Idea Center** System: This project focuses on automating tests for the "Idea Center" system, a platform where users can submit and discuss innovative ideas.
- **Foody** System: The "Foody" system project involves automating tests for a food review service API. 
- **Story** Spoiler System: The project automates tests for the "Story Spoiler" system, an application that provides users with spoilers for their favorite stories.

## Test Case Coverage
- User authentication and authorization
- User data submission and retrieval
- User data edition and deletion
- Error handling and validation
  
## Technologies Used
- **RestSharp**: A simple REST and HTTP API client for .NET.
- **NUnit**: A unit-testing framework for all .NET languages.

## Project Structure
- **Tests**: Contains the test cases for each system.
- **Models**: Contains the models representing the API responses and requests.

## RestClient Initialization and Configuration
1. **Initialize a RestClient with the base URL of the API.**
2. **Authenticate with your credentials, and store the received JWT token.**
   - Authenticate by sending a request with your credentials.
   - Capture the JWT token from the response and store it securely for subsequent requests.
3. **Configure the RestClient with an Authenticator using the stored JWT token.**
   - Use the stored JWT token to configure the RestClient's Authenticator.
   - This setup ensures that all subsequent requests made by the RestClient are authenticated.

### Contributing
Contributions are welcome! If you have any improvements or bug fixes, feel free to open a pull request.

### License
This project is licensed under the [MIT License](LICENSE). See the [LICENSE](LICENSE) file for details.
### Contact
For any questions or suggestions, please open an issue in the repository.

---
### Happy Testing! 🚀
