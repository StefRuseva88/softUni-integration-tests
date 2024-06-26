
# SoftUni-Projects - RestSharp API Tests 
![image](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![image](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![image](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
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
