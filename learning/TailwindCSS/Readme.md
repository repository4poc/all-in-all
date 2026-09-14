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

`Examples`

## Text Size

```
      <p className='text-3xl'>Hello</p>

```

## Text Color

```
      <p className='text-blue-500'>About</p>

```

![alt text](images/{40381A26-676E-49B0-A912-BBC01AB2298B}.png)

## font

```
font-bold
font-light
font-thin
```

```
      <p className='text-3xl text-blue-700 font-bold'>{count}</p>
```

## Background

```
bg-blue-700

bg-red-500
```

## Padding

```
p-1

p-0

pb-1

pt-2

pr-2

pl-2

px-1

py-1
```

```
      <button className='bg-blue-700 p-1' onClick={handleCount}>
        Count
      </button>

```

## Rounded

```
      <button className='bg-blue-700 rounded' onClick={handleCount}>
        Count
      </button>
```

## flex

This makes the element a flex container, so its children are arranged in a row by default.

![alt text](images/{897AF530-0C2E-4E94-A606-B71158FDC044}.png)

![alt text](images/{0BB177A4-DEBD-48EC-A9AD-FF55103A6E1A}.png)

```
flex            // display: flex
flex-row        // horizontal (default)
flex-col        // vertical
items-center    // align children vertically in center
justify-center  // align children along main axis center
gap-4           // spacing between children
```

```
flex  gap-x-9

flex flex-col items-center gap-x-9

flex flex-row items-center gap-y-9
```

```
    <div className='flex flex-col items-center'>
      <p className='text-3xl text-blue-700 font-bold'>{count}</p>
      <button className='bg-blue-700 rounded p-1' onClick={handleCount}>
        Count
      </button>
    </div>
```

## justify-between

puts equal space between flex items.

![alt text](images/{DFA45460-7180-42EB-A2D2-4D07F71923DC}.png)

![alt text](images/{B4F0BEBB-D753-4E7D-AC36-A36657D51B8F}.png)

## Grid

grid grid-cols-\* and flex flex-col are different layout systems.

- flex flex-col

  Uses Flexbox and arranges items in a single column.

  ![alt text](images/{81048081-B66F-4E72-A8A1-ABDEC42EEE65}.png)

- grid grid-cols-\*

  Uses CSS Grid and arranges items into rows and columns.

  ![alt text](images/{AAEA0653-0047-4BFF-AE52-09BF562BB946}.png)

```
grid md:grid-cols-2 gap-8
```

Equivalent to

```
grid lg:grid-cols-12 gap-4
```

grid-cols-12 is a Tailwind CSS utility class that defines a grid with 12 equal-width columns.

```
| 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |10 |11 |12 |
```

```
<div className="grid grid-cols-12 gap-4">
  <div className="col-span-3">Sidebar</div>
  <div className="col-span-9">Main Content</div>
</div>
```

```
<section>
    <h1 className='text-blue-900 font-bold text-4xl'>Tour List</h1>
    <div className='grid md:grid-cols-2 gap-8'>
        {
            data.map((tour) => {
                return (
                    <div id={tour.id} key={tour.id}>
                        <Link href={`/tours/${tour.id}`}>{tour.name}</Link>
                    </div>
                );
            })
        }
    </div>
</section>

```

![alt text](images/{A866C002-B737-407D-B0CF-A546F36B9D44}.png)

## Gap : Gap between containing items

```
gap-x-9
```

```
    <nav className='py-4 flex gap-x-8'>
      <Link href='/'>Home</Link>
      <Link href='/about'>About</Link>
      <Link href='/contact'>Contact</Link>
      <Link href='/counter'>Counter</Link>
      Counter
    </nav>
```

## Margin

```
mt-1

mb-2

mx-2

my-2

mx-auto  - Bring it at center on x-axis
```

## MAX WIDTH

```
max-w-7xl  (Full Width of a laptop screen)

max-w-3xl

max-w-2xl
```

This below combination bring the div with width `max-x-3xl` at center of screen

```
mx-auto max-x-3xl
```

![alt text](images/{21EE4BC6-67D8-44A0-B1BE-50465CE6C791}.png)

## Border

```
border-[color]-[10-700]

border-red-300

border-[0-7]

```

## rounded object-cover

```
<Image
    src={mapsImg}
    alt={tour.name}
    className='rounded object-cover'
    >
</Image>
```

## Capitalize

It makes the first letter of each word uppercase.

```
<p className="capitalize">hello world</p>
```

**Displays as:**

Hello World

```
      <Button variant='default' size='sm' className='capitalize'>
        default variant
      </Button>
```

**Display**

![alt text](images/{B1B37DDD-ACCB-4788-A02B-422F30CAD921}.png)

## mx-auto

mx-auto only works if the element has a width smaller than its container

The x stands for the horizontal axis (left and right margins).

```
<div className="w-64 mx-auto bg-blue-200">
  Centered box
</div>
```

This creates a box with width 16rem (w-64) and centers it horizontally within its parent.

## max-w-6xl

Keep this content no wider than 1152px

| Class       | Width          |
| ----------- | -------------- |
| `max-w-xl`  | 36rem (576px)  |
| `max-w-2xl` | 42rem (672px)  |
| `max-w-4xl` | 56rem (896px)  |
| `max-w-6xl` | 72rem (1152px) |
| `max-w-7xl` | 80rem (1280px) |

So `max-w-6xl mx-auto` is essentially saying:

Keep this content no wider than 1152px, and center it on the page

| Class       | Effect                                                        |
| ----------- | ------------------------------------------------------------- |
| `max-w-6xl` | `max-width: 72rem` (1152px)                                   |
| `mx-auto`   | Centers the element horizontally (`margin-left/right: auto`)  |
| `px-1`      | Adds horizontal padding (`padding-left/right: 0.25rem` = 4px) |

## cn()

cn() often uses tailwind-merge, it can intelligently resolve conflicting Tailwind classes

```
cn("px-2", "px-4")
```
