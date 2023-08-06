# CityInfoWebAPI

Welcome to CityInfoWebAPI, a RESTful web service for managing city information. This README provides an overview of the project's structure, components, usage, and configuration.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
  - [API Documentation](#api-documentation)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)
- [Unit Testing](#unit-testing)
- [Contributing](#contributing)
- [License](#license)

## Introduction

CityInfoWebAPI is a powerful web service that allows users to manage city information, including details such as name, country, population, and timezone. The API provides a range of endpoints for retrieving, creating, updating, and deleting city data, making it a versatile tool for various applications.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/) running locally or accessible via a connection string

### Installation

1. Clone the repository to your local machine:
- git clone https://github.com/yourusername/CityInfoWebAPI.git

2. Navigate to the project directory:
- cd CityInfoWebAPI

3. Configure MongoDB settings in `appsettings.json`.

4. Build and run the project:
- dotnet run --project CityInfoWebAPI

## Usage

Once the project is running, access the API documentation to explore available endpoints and their functionalities.

### API Documentation

Navigate to [http://localhost:{PORT}/swagger](http://localhost:{PORT}/swagger) in your web browser to interact with the API documentation. This interactive UI allows you to test endpoints, view responses, and understand the API's capabilities.

## API Endpoints

The API provides the following endpoints:

- **GET /cities**: Retrieves a list of all cities.
- **GET /cities/{id}**: Retrieves information about a specific city by ID.
- **POST /cities**: Creates a new city entry.
- **PUT /cities/{id}**: Updates an existing city by ID.
- **DELETE /cities/{id}**: Deletes a city by ID.

## Configuration

The `appsettings.json` file contains configuration settings for the project, including logging levels, allowed hosts, and MongoDB database connection details. Modify this file to tailor the application behavior to your needs.

## Unit Testing

The `CitiesControllerTests` class includes unit tests that ensure the correctness of the `CitiesController` behavior. These tests validate endpoint functionality using the xUnit testing framework, FluentAssertions, and Moq.

## Contributing

Contributions to CityInfoWebAPI are welcome! To contribute:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Implement your changes and ensure they're thoroughly tested.
4. Create a pull request with a clear description of your changes.

## License

CityInfoWebAPI is released under the [MIT License](LICENSE).
