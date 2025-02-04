<div align="left" style="position: relative;">
<img src="https://raw.githubusercontent.com/PKief/vscode-material-icon-theme/ec559a9f6bfd399b82bb44393651661b08aaf7ba/icons/folder-markdown-open.svg" align="right" width="30%" style="margin: -20px 0 0 20px;">
<h1><code>Livefront Code Challenge</code></h1>
<p align="left">
	<em>Here are a few slogan ideas that capture the essence of the project:

**"Code with Certainty, Deliver with Confidence"**</em>
</p>
<p align="left">
	<!-- Shields.io badges disabled, using skill icons. --></p>
<p align="left">Built with the tools and technologies:</p>
<p align="left">
<img src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" alt="CSharp" />
<img src="https://img.shields.io/badge/NuGet-004880.svg?style={badge_style}&logo=NuGet&logoColor=white" alt=".Net" />	
<img src="https://img.shields.io/badge/Docker-2CA5E0.svg?style={badge_style}&logo=docker&logoColor=white" alt="Docker" />
<img src="https://img.shields.io/badge/NuGet-004880.svg?style={badge_style}&logo=NuGet&logoColor=white" alt="NuGet">
	</p>
</div>
<br clear="right">

## 🔗 Quick Links

- [📍 Overview](#-overview)
- [👾 Features](#-features)
- [🚀 Getting Started](#-getting-started)
  - [☑️ Prerequisites](#-prerequisites)
  - [⚙️ Installation](#-installation)
  - [🤖 Usage](#🤖-usage)
  - [🧪 Testing](#🧪-testing)
- [🔰 Seed Data](#-seed-data)
- [📌 API Reference](#-api-reference)
- [📌 Additional Endpoints](#-additional-endpoints)

---

## 📍 Overview

This repository showcases my response to a coding challenge presented by Livefront. The task involved developing an API specification for a new referrals feature.

---

## 👾 Features

|      | Feature         | Summary       |
| :--- | :---:           | :---          |
| ⚙️  | **Architecture**  | <ul><li>The project uses a microservices architecture, with separate services for web API, database, and other components.</li><li>The solution file (`LiveFrontCodeChallenge.sln`) defines the relationships between various components and configurations.</li><li>Containerization is used through Docker Compose to manage dependencies between services and enable seamless interaction with the database from the web application.</li><li>Use of NuGet package manager for dependencies</li></ul> |
| 🔩 | **Code Quality**  | <ul><li>The project uses a consistent naming convention and follows standard coding practices.</li><li>C# is the primary language.</li><li>The codebase includes unit tests, integration tests, as well as architecture tests, which are utilized to establish and maintain code quality standards including design conventions and project dependencies in the CartonCaps.Tests project.</li></ul> |
| 📄 | **Documentation** | <ul><li>The documentation is primarily in Markdown format and covers topics such as installation, usage, and testing.</li><li>The `docker-compose.yml` file provides a clear overview of the containerized environment and its dependencies.</li></ul> |
| 🔌 | **Integrations**  | <ul><li>NuGet package manager integration for dependencies</li><li>The project integrates with Docker Compose to enable containerized development and deployment.</li><li>Integration with the SQL database is established through `docker-compose.yml` configuration.</li></ul> |
| 🧩 | **Modularity**    | <ul><li>The project uses a modular architecture, with separate services with clear relationships between them, promoting modularity and maintainability.</li><li>The use of containerization through Docker Compose enables the deployment of individual services without affecting others.</li><li>The project includes separate services for web API, services, and tests, ensuring a clean separation of concerns.</li><li>The project used the NUnit3 testing framework for test execution.</li></ul> |
| 🧪 | **Testing**       | <ul><li>The project contains unit tests, integration tests, and architecture tests to ensure the correctness of the codebase.</li><li>The use of containerization enables easy deployment and testing of individual services.</li></ul> |
| ⚡️  | **Performance**   | <ul><li>High code quality with a mix of C# and JSON files suggests good performance</li><li>Use of NuGet package manager for dependencies supports performance by allowing efficient management of dependencies</li><li>Presence of various file types indicates potential for optimization and performance tuning</li></ul> |
| 🛡️ | **Security**      | <ul><li>NuGet package manager integration for dependencies suggests good security practices (e.g., dependency management)</li><li>Presence of configuration files (appsettings.json) indicates some level of security and access control</li><li>Use of various file types (csproj, cs, json, etc.) suggests potential for secure coding practices</li></ul> |

---

## 🚀 Getting Started

### ☑️ Prerequisites

Before getting started with LiveFrontCodeChallenge, ensure your runtime environment meets the following requirements:
- **Version Control:** [Git](https://git-scm.com/downloads)
- **Programming Language:** [CSharp](https://learn.microsoft.com/en-us/dotnet/csharp/)
- **Programming Framework:** [.NET8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **Package Manager:** [Nuget](https://learn.microsoft.com/en-us/nuget/)
- **Container Runtime:** [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### ⚙️ Installation

Install  using one of the following methods:

**Build from source:**

1. Clone the  repository:
```sh
git clone https://github.com/goeke-m/LiveFrontCodeChallenge.git
```

2. Navigate to the project directory:
```sh
cd ./LiveFrontCodeChallenge
```

**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
dotnet restore LiveFrontCodeChallenge.sln
```
**Using `docker`** &nbsp; [<img align="center" src="https://img.shields.io/badge/Docker-2CA5E0.svg?style={badge_style}&logo=docker&logoColor=white" />](https://www.docker.com/)
 
- From the root project directory

```sh
docker compose  -f "./docker-compose.yml" -f "./docker-compose.override.yml" -p livefrontcodechallenge build
```

### 🤖 Usage
<details>
	<summary><b>Command Line Interface</b></summary>
	<b>Using `docker`<b> &nbsp; [<img align="center" src="https://img.shields.io/badge/Docker-2CA5E0.svg?style={badge_style}&logo=docker&logoColor=white" />](https://www.docker.com/)

- From the root project directory

```sh
docker compose  -f "./docker-compose.yml" -f "./docker-compose.override.yml" -p livefrontcodechallenge up
```
- This will start the API and SQL Server containers.
- API Swagger Endpoint: `https://localhost:8081/swagger/index.html`.
- The API will be available at `https://localhost:8081/api/v1/referral`.
- The API's liveness can be checked at `https://localhost:8081/api/v1/health`. A 200 response indicates the API is up and running.
</details>

<details>
	<summary><b>Visual Studio</b></summary>
</details>


### 🧪 Testing
Run the test suite using the following command:

<detail>
	<summary><b>Command Line Interface</b></summary>
		<b>Using `nuget`</b> &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

- From the root project directory

```sh
dotnet test ./CartonCaps.Tests/CartonCaps.Tests.csproj 
```
</detail>

<details>
	<summary><b>Visual Studio</b></summary>
</details>

---

## 🔰 Seed Data

```
[
    {
        "referee": {
			"id": "143c90ed-83c2-4ca7-9c07-24957cfadddf",
            "firstName": "Marsha",
            "lastName": "Mellow",
            "phoneNumber": "555-908-4836",
            "email": null
        },
        "referralStatus": "Complete",
        "referralCode": "X5YGP01"
    },
    {
        "referee": {
            "id": "d69171d3-5a79-464b-9ac3-6dc220c07e30",
			"firstName": "Gene",
            "lastName": "Pool",
            "phoneNumber": null,
            "email": "genepool@gmail.com"
        },
        "referralStatus": "Pending",
        "referralCode": "X5YGP01"
    },
    {
        "referee": {
			"id": "24278723-2248-48da-a6f2-c7ba4056a144",
            "firstName": "Willie",
            "lastName": "Makeit",
            "phoneNumber": null,
            "email": "williemakeit@gmail.com",
            "referral": null
        },
        "referralStatus": "Complete",
        "referralCode": "X5YGP01"
    }
]
```
---

## 📌 API Reference

The Referrals API is built using ASP.NET Core and provides the following functionality:

- Manage referrals, including creating, retrieving, updating, and deleting referrals.
- Manage referees, including creating, retrieving, updating, and deleting referees.
- Retrieve referrals based on various criteria, such as referral code and referral status.

The API uses Entity Framework Core for data access and SQL Server as the database.

The Referrals API provides the following endpoints:

### Get Referrals
- **Endpoint**: `GET /api/v1/referral/getreferrals`
- **Parameters**:
  - `referralCode` (required): The referral code to filter the referrals.
  - `referralStatus` (optional): The referral status to filter the referrals to a specific status.
- **Response**: A list of referrals matching the provided criteria.

### Get Referral by ID
- **Endpoint**: `GET /api/v1/referral/getreferralbyid/{id}`
- **Parameters**:
  - `id` (required): The unique identifier of the referral.
- **Response**: The details of the referral with the specified ID.

### Create Referral
- **Endpoint**: `POST /api/v1/referral/createreferral`
- **Request Body**:
  - `firstName` (required): The first name of the referee.
  - `lastName` (required): The last name of the referee.
  - `phoneNumber`: The phone number of the referee. Either phone number or email is required.
  - `email` : The email of the referee. Either phone number or email is required.
  - `referralStatus` (optional): The status of the referral. This will default to pending status.
  - `referralCode` (required): The referral code.
- **Response**: The details of the created referral.

### Update Referral Status
- **Endpoint**: `PATCH /api/v1/referral/updatereferralstatus`
- **Request Body**:
  - `referralId` (required): The unique identifier of the referral.
  - `referralStatus` (required): The new status of the referral.
- **Response**: The updated referral details.

### Update Referral
- **Endpoint**: `PUT /api/v1/referral/updatereferral`
- **Request Body**:
  - `referralId` (required): The unique identifier of the referral.
  - `firstName` (required): The updated first name of the referee.
  - `lastName` (required): The updated last name of the referee.
  - `phoneNumber` (required): The updated phone number of the referee.
  - `email` (required): The updated email of the referee.
  - `referralStatus` (required): The updated status of the referral.
  - `referralCode` (required): The updated referral code.
- **Response**: The updated referral details.

## 📌 Additional Endpoints:
- **Endpoint**: `GET /api/v1/health`
- **Response**: 200 Status Code if Api is up and running.
---

## Scenario Considerations
Several design assumptions were made in drafting this API specification. Additionally, pertinent questions were considered during development. These questions and their corresponding answers related to the API will be presented in a Q&A format below.

### How will existing users create new referrals using their existing referral code?
Existing users can create new referrals by incorporating their referral `code` into the request body when sending a POST request to the /createreferral endpoint. The API will validate the request and register the new referral using the provided code.

### How will referral links There are three ways included in the scenario to share a referral:

1. **Share via Text**: A pre-populated text message sent via SMS or other text messaging solution.
2. **Share via Email**: A pre-populated email sent via email client.
3. **Share via Share Sheet**: Integration with the device's share sheet to allow sharing via any app that supports sharing or quick copy of the link.be shared?

In a production environment, the processes for sharing via text and email would be automated. In this API, these functionalities are simulated by including fields for email and phone numbers in the Referee model. Requiring at least one of the two in order to create a new referral.

The share sheet method will be handled by the client application, which is beyond the scope of this API. This approach also considers scenarios where the referee's email address or phone number might not be available.