## Tailwind CSS

A CSS Framework

## Advantages of Tailwind CSS

1.  Faster UI Development

    Instead of switching between HTML and CSS files, you build designs directly with utility classes.

    ```
    <div class="flex items-center justify-between p-4">
            ...
    </div>
    ```

2.  No Unused Components

    Bootstrap ships with many components and styles you may never use.

    Tailwind generates only the CSS classes you actually use (via Purge/Content scanning), resulting in smaller production bundles.

3.  Excellent for React, Vue, and Next.js

    Tailwind fits naturally into component-based frameworks.

    ```
    <button className="bg-blue-600 text-white px-4 py-2 rounded">
    Save
    </button>
    ```

4.  Responsive Design is Easy

    Responsive classes are built in.No need to write custom media queries for many common cases.

    ```
    <div class="text-sm md:text-lg lg:text-xl">
    Responsive text
    </div>
    ```

5.  Dark Mode Support

    Tailwind provides built-in dark mode utilities

    ```
    <div class="bg-white dark:bg-gray-900">
    Content
    </div>
    ```

6.  Large Ecosystem

    The Tailwind ecosystem includes tools such as:
    - Tailwind CSS
    - Tailwind UI
    - Flowbite
    - DaisyUI

## When to use Tailwind

Tailwind is usually the stronger choice for:

- SaaS applications
- Dashboards
- Startup products
- React/Next.js projects
- Custom UI designs
- Large frontend codebases

Bootstrap is often preferable when you need a polished interface quickly with minimal design work. Tailwind is generally better when you want complete control over the final look and feel.

## Installation

https://tailwindcss.com/docs/installation/using-vite

1.  Create Vite Project

    ```
    npm create vite@latest <project-name>

    Choose Options
    - ReactJS
    - TypeScript
    - ESLite
    ```

2.  Install TalwindCSS

    ```
    npm install tailwindcss @tailwindcss/vite
    ```

3.  Configure the Vite plugin

    Add the @tailwindcss/vite plugin to your Vite configuration (vite.config.ts)

         ```
         import { defineConfig } from 'vite'
         import tailwindcss from '@tailwindcss/vite'

         export default defineConfig({
            plugins: [
                tailwindcss(),
            ],
         })
         ```

4.  Import Tailwind CSS

    Add an @import to your CSS file that imports Tailwind CSS.

    /src/style.css

    ```
    @import "tailwindcss";
    ```

    ![alt text](images/{0D9F8AED-B6A9-4832-BE9B-D83F1BE0EBA2}.png)

5.  Start your build process

    Run your build process with npm run dev or whatever command is configured in your package.json file.

    ```
    npm run dev
    ```

6.  Start using Tailwind in your App.tsx

    ```
    <h1 class="text-3xl font-bold underline">
        Hello world!
    </h1>
    ```

    ![alt text](images/{C083068F-C2EC-46E0-AB35-0CFEEDE42899}.png)

## Install Additional packages

```
npm install nanoid react-icons
```

1. nanoid : A tiny library for generating unique IDs.

   ```
   import { nanoid } from 'nanoid';

   const id = nanoid();

   console.log(id);
   // V1StGXR8_Z5jdHi6B-myT
   ```

   Common uses:
   - Unique keys for React lists
   - IDs for tasks, notes, todos
   - Temporary client-side identifiers
   - Generating share codes or tokens

2. react-icons : A library that provides popular icon sets as React components.

   ```
   import { FaGithub } from 'react-icons/fa';
   import { MdEmail } from 'react-icons/md';

   function Contact() {
   return (
       <div>
       <FaGithub />
       <MdEmail />
       </div>
   );
   }
   ```

   You get icons from many libraries:
   - Font Awesome (fa)
   - Material Design (md)
   - Bootstrap Icons (bs)
   - Heroicons (hi)
   - Remix Icons (ri)
   - Feather Icons (fi)

   ![alt text](images/{35762359-9CB2-4955-A3B4-CD97720392CF}.png)

## UUID vs NanoID

uuid and nanoid solve a similar problem (generating unique IDs), but many modern React projects prefer nanoid for a few reasons:

| Feature          | nanoid                   | uuid                          |
| ---------------- | ------------------------ | ----------------------------- |
| Bundle size      | Very small (~130 bytes)  | Larger                        |
| Performance      | Fast                     | Fast                          |
| Security         | Cryptographically secure | Cryptographically secure (v4) |
| URL-friendly IDs | Yes                      | No (contains hyphens)         |
| React projects   | Very popular             | Popular                       |
| Readability      | Short IDs                | Long IDs                      |

```
import { v4 as uuidv4 } from 'uuid';

console.log(uuidv4());
// 550e8400-e29b-41d4-a716-446655440000
```

```
import { nanoid } from 'nanoid';

console.log(nanoid());
// V1StGXR8_Z5jdHi6B-myT
```

Notice that NanoID generates a shorter, URL-friendly string.

Use uuid when:

- You must follow the UUID standard.
- Your backend/database expects UUIDs.
- You're integrating with systems that specifically require UUID v4.

Example:

- PostgreSQL UUID columns
- Enterprise APIs
- Distributed systems using UUIDs as identifiers

## Tailwind Useful Extensions

1. Tailwind CSS Intellisense
   ![alt text](images/{8D098C72-4407-401C-96D9-640D6B3C2BAB}.png)
2. Tailwind Fold
   ![alt text](images/{6BDDCE08-BCF4-494E-BE35-CCFFE3A906CD}.png)

## In JSX use single Quotes instead of double Quotes

![alt text](images/{0DFB64EB-4238-449F-819F-8F13C5B326EB}.png)

Update Prettier Plugins Settings

![alt text](images/singleQuotePrettier.png)

## Margin

- mx-auto
  ![alt text](images/{7AD4643F-8E7B-4B80-80B2-1DB6020FF4D8}.png)

- mr-6
  ![alt text](images/{1DFD6484-F50C-4968-A1FE-28F44DDF902F}.png)

## Padding

- p-8

  ![alt text](images/{60CBA3D2-2D7E-42BE-A2D1-CA9710EF5918}.png)

- px-8
- py-8

  ![alt text](images/{4EB1D9FE-2CA0-4AED-A00C-554FD411F8D4}.png)

## Max-Width

- max-w-7xl

  ![alt text](images/{684A1521-79D3-441F-B7E3-1E28428395E4}.png)

## flex

Utilities for controlling how flex items both grow and shrink.

## flex-direction

Utilities for controlling the direction of flex items.

![alt text](images/{03AD150E-763F-4867-9CD7-728FA80D0D90}.png)

![alt text](images/{95E40EAE-F069-47AE-97F7-84C0322B10A7}.png)

![alt text](images/{F1F9C5F6-4B3E-4199-AEAF-36A1EE179584}.png)

`sm:flex-row`

![alt text](images/{53BF257F-155F-43A1-9EE0-9D44EC59A6EF}.png)

## gap

Utilities for controlling gutters between grid and flexbox items.

![alt text](images/{7ED84B18-E400-46BC-B8F7-01A5D5979AA2}.png)

`sm:gap-x-20`

![alt text](images/{F4C585E1-77A3-4346-891C-B98E86AE7D4E}.png)

## font-size

`text-3xl`

![alt text](images/{630A74E8-4DD4-46D6-9B3D-B7C6A0818E84}.png)

## font-weight

`font-bold`

![alt text](images/{2CE2F919-2DBA-428A-BB0E-21FA38B51E6C}.png)

| Tailwind Prefix | Min Width | Typical Devices                                            |
| --------------- | --------- | ---------------------------------------------------------- |
| (default)       | 0px       | Small phones                                               |
| `sm:`           | 640px     | Large phones, small tablets, most laptops and desktops too |
| `md:`           | 768px     | Tablets (portrait), laptops                                |
| `lg:`           | 1024px    | Laptops, desktops, tablets (landscape)                     |
| `xl:`           | 1280px    | Large laptops, desktop monitors                            |
| `2xl:`          | 1536px    | Large desktop monitors, ultrawide screens                  |

![alt text](images/{BD36C61D-165C-417C-81E8-2DD135366AF9}.png)
