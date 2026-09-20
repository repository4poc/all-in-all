## Supabase and Prisma

- Supabase = PostgreSQL database + backend services
  1. create an organizaiton
  2. create a project within the organization
  3. choose DB Password
- Prisma = ORM that your application uses to talk to that PostgreSQL database.

```
Your app
   │
   ├── Prisma ORM
   │       │
   │       ▼
   │   Supabase PostgreSQL
   │
   └── Supabase Auth / Storage / Realtime
```

```
npm install prisma@6 --save-dev
npm install @prisma/client@6

```

![alt text](images/{6994B140-D0CE-4B20-98AC-2CC89AC160E6}.png)

```
npx prisma@6 init
```

This create a `prisma/schema.prisma` file at project root

1. It creates a `prisma/schema.prisma`

   ![alt text](images/{FE81980D-4FC5-4E3A-9BE3-34173301DF06}.png)

2. Update the `.env`

   ![alt text](images/{E48EE766-EB9A-4028-8DE4-07A7E3382264}.png)

## Prisma VS Code Extension

![alt text](images/{D205DE67-6CC3-4BC2-9B47-BB7BE1A7AD17}.png)

## Connect Prisma to Supabase

1. Create `utils/db.tsx`

```
import { PrismaClient } from '@prisma/client';

const prismaClientSingleton = () => {
  return new PrismaClient();
};

type PrismaClientSingleton = ReturnType<typeof prismaClientSingleton>;

const globalForPrisma = globalThis as unknown as {
  prisma: PrismaClientSingleton | undefined;
};

const prisma = globalForPrisma.prisma ?? prismaClientSingleton();

export default prisma;

if (process.env.NODE_ENV !== 'production') globalForPrisma.prisma = prisma;

```

2. Add into `.env`
   - DATABASE_URL=
   - DIRECT_URL=

   ![alt text](images/{C0312A14-EB51-40E0-BB89-D5DBE80FE550}.png)

3. Get the Info and update the `.env` file
   - Click Supabase `Connect`
     ![alt text](images/{1E241816-2B3F-43FC-B22E-6BCBF4434B84}.png)

4. Update the `prisma/schema.prisma`

   ```
   generator client {
   provider = "prisma-client"
   output   = "../lib/generated/prisma"
   }

   datasource db {
   provider = "postgresql"
   }

   model Product {
   id  String   @id @default(uuid())
   name  String
   company String
   description String
   featured String
   image String
   price Int
   createdAt DateTime @default(now())
   updatedAt DateTime @updatedAt
   clerkId String
   }

   ```

5. Update the `prisma.config.ts`

   ```
   import 'dotenv/config';
   import { defineConfig, env } from 'prisma/config';

   export default defineConfig({
   schema: 'prisma/schema.prisma',

   datasource: {
      url: env('DIRECT_URL'),
   },
   });

   ```

6. Push `prisma/schema.prisma` changes into the `Supabase` Database

   ```
   npx prisma db push
   ```

7. How to access Supabase database using Prisma

   ```
   $ npx prisma studio

   ```

8. Seed Data

   ```
   $ npx tsx prisma/seed.ts
   ```
