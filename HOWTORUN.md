## Run it

### First time only
1. In the API folder, create `.env` (copy `.env,example`) and set one password
   (8+ characters with upper case, lower case and a symbol; avoid `$` and `;`).
   Use the same password in both lines.
2. `docker compose up -d` (Docker Desktop must be running). Wait about 30 seconds for SQL Server to start.
3. `dotnet ef database update` to create the tables.
4. In the frontend folder: `npm install`

### Every time
1. Start Docker Desktop, then in the API folder: `docker compose up -d`
2. Terminal 1, API folder: `dotnet run`
3. Terminal 2, frontend folder: `npm run dev`
4. Open http://localhost:5173

### Register a user first
There's no login or register page yet, so open http://localhost:5144/swagger,
use `POST /api/Auth/register`, and create a user. The frontend currently creates
tickets as `userId: 1`, so the first registered user must have Id 1.