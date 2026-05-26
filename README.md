# School Property Evidence

A two-project solution for managing school property.

- **Backend** (`SchoolPropertyEvidence/`) - ASP.NET Core Web API (.NET 8), handles data and authentication
- **Frontend** (`Frontend/SchoolProperty.Web/`) - Blazor Server app (.NET 9), the user interface

---

## Changes Since Last Commit

### 1. Folder structure reorganised

The project was restructured. The old MVC backend (with its Bootstrap/jQuery static files) was replaced by a clean API-only backend. A brand new `Frontend/` folder was added containing the Blazor app. The solution file (`SchoolPropertyEvidence.sln`) was also added at the root.

### 2. Login page now uses the real backend

**File:** `Frontend/SchoolProperty.Web/Components/Pages/Login.razor`

Previously the login page just checked a hardcoded email and password (`student@test.com` / `1234`) - nothing was actually sent to the server.

Now it:
- Sends the email and password to the backend at `POST api/auth/login`
- Gets back a JWT token (a secure login ticket)
- Saves that token in the browser localStorage so the app remembers you are logged in
- Redirects you to `/home` on success, or shows an error on failure

### 3. Register page connects to the backend

**File:** `Frontend/SchoolProperty.Web/Components/Pages/Register.razor`

The register page sends your first name, last name, email and password to `POST api/auth/register`. It shows a success or error message depending on the response.

### 4. Port conflict fixed - frontend now runs on different ports

The frontend and backend were both trying to use port `7223`, which caused them to clash.

The frontend is now configured to run on:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

This is set in `Frontend/SchoolProperty.Web/appsettings.json` (Kestrel section) and `Frontend/SchoolProperty.Web/Properties/launchSettings.json`.

The backend continues to run on `https://localhost:7223`.

### 5. Backend configuration

**File:** `SchoolPropertyEvidence/appsettings.json`

- **Database**: connects to the school MySQL server (`mysqlstudenti.litv.sssvt.cz`)
- **JWT**: secret key and issuer name are set here - these are used to sign and verify login tokens
