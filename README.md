📄 Document Sharing API

A secure document sharing system built with ASP.NET Web API that allows files to be accessed through temporary, signed access tokens instead of direct URLs.

This project demonstrates backend security practices, token-based authorization, and protected file storage — making it ideal for learning production-style API design.

🚀 Features
Core Functionality

Upload documents securely

Generate time-limited access tokens

Download files only with valid tokens

Block direct file access

Store files outside public folders

Security Features

Tokens stored as hashes (not plaintext)

Expiration enforced on every request

Optional one-time token usage

Token revocation support

Download tracking per token

Bonus Capabilities

Track download counts

Manual token revocation

Extensible to cloud storage later

🧱 Tech Stack

ASP.NET Web API (.NET 8 / .NET 9 compatible)

Entity Framework Core

Local file storage

OpenAPI / Swagger / Scalar UI support
