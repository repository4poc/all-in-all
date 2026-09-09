## Next.js

A React Framework for createing high quality web applications

| Project               | Good choice             |
| --------------------- | ----------------------- |
| Simple frontend / SPA | React + Vite            |
| Full-stack web app    | Next.js                 |
| SEO-heavy website     | Next.js                 |
| E-commerce            | Next.js                 |
| Dashboard/admin app   | React + Vite or Next.js |
| Need SSR/SSG          | Next.js                 |
| Learning React        | React + Vite            |

- An SEO-heavy website is a website where getting traffic from Google and other search engines is very important.

  SEO = Search Engine Optimization.

  For example:
  - News website
  - Blog
  - E-commerce/product website
  - Real-estate website
  - Travel website
  - Restaurant directory
  - Documentation/knowledge website

    Imagine you have a page:

        "Best laptops under $1,000"

    You want someone searching Google for that phrase to find your page.

- An e-commerce app is an application that lets users browse, buy, and manage products online.

```
E-commerce App
│
├── Home
├── Products
│   ├── Product list
│   └── Product details
│
├── Cart
│   ├── Add product
│   ├── Remove product
│   └── Change quantity
│
├── Checkout
│   ├── Address
│   ├── Payment
│   └── Order confirmation
│
├── Account
│   ├── Login/Register
│   └── My orders
│
└── Admin
    ├── Products
    ├── Orders
    └── Customers
```

```
Frontend
Next.js + React
       ↓
Data fetching
TanStack Query (React Query)
       ↓
Backend
Next.js API / separate backend
       ↓
Database
PostgreSQL / MySQL / MongoDB
       ↓
Payments
Stripe / similar payment provider
```

```
→ Next.js handles the website and React UI
→ TanStack Query handles client-side API data
→ backend handles business logic
→ database stores products/orders/users.
```

## Create NextJS project

```
npx create-next-app full-stack-app
```

```
npm run dev
```

URL : Localhost:3000

![alt text](images/{111E3767-97F6-4A27-8138-C4FC85B818CA}.png)

- `page.tsx` in the `app` folder represents the root of our application.

Return the Home component from the `page.tsx`

![alt text](images/{DFBAE561-2C10-44E4-82C2-0881C2F5288B}.png)

Similarly we can have other component's specific `page.tsx`

![alt text](images/{359CB6F3-E5EC-4A6C-8AD8-2815970683EB}.png)

Include it in the `app/page.tsx`

![alt text](images/{E9A36CF2-D7E4-41F0-BC4F-B9AEDECF56F9}.png)

## To Open Browser in VS Code

![alt text](images/{50A13938-FB64-41AD-88C2-2571DF3047EB}.png)

## Nested Routes

By default the component the folder structure pattern in the URL

![alt text](images/{C0AC8B1D-9097-4796-9B47-3E41D757E3D5}.png)

![alt text](images/{876052A4-45BC-4780-8E00-A6689D82251A}.png)

![alt text](images/{E40688B7-81D5-4E00-AEE2-7386693D35C5}.png)

In case we want the URL to be /info/contact we need to place the `contact/page.tsx` under `/app/info/contact/page.tsx`

## Global.css

Put all your CSS in this file,

Using `@import "tailwindcss";` to import TailwindCSS, which is widely used as company standard nowadays

![alt text](images/{4DC6E558-0422-425D-B5B1-64D61652CF61}.png)

## Layout File

We have a default `layout.tsx` remove the content from it

![alt text](images/{76F2E1EB-21F0-4523-8CB5-5D516EC3F021}.png)

This is the `root layout`, it does not rerender, when we go from page to page, but the `template layout` does rerender.

![alt text](images/{9D46F37E-5C3E-4257-BE23-05A4D0999688}.png)

![alt text](images/{A9197F86-6702-4318-8C81-702E8B085A34}.png)

![alt text](images/{1C987718-C9C9-4885-AC06-70D9E6855F18}.png)

So `Nav Bar` remains there and the {childen} changes

- / : Page.tsx - Home Component
- /about : Page.tsx - About Component

## Nav Component

- create /components/Navbar.tsx
- render in layout.tsx

Note : If we want our components do not become the route, put it outside `/app`, parallel to it

![alt text](images/{3EC6D98D-A278-4077-A79F-975DC528226E}.png)

## Use Google Fonts

Automatically self-host any Google Font, Fonts are included in the deployment and served from the same domain as your deployment, No requests are sent to Google by the browser.

![alt text](images/{3FB089AC-789B-4972-8F6A-A72739BFBCB8}.png)

![alt text](images/{72E3E303-5C4F-4757-AF61-4B68C753F56F}.png)

![alt text](images/{9E8C1B2C-CF4D-4D58-ADA5-24763A906F0E}.png)

## Metadata

Next.js has a Metadata API that can be used to defind your application metadata for improved SEO and Shareability.

![alt text](images/{C83DF9D3-EDFB-49FF-8D29-4E19486E0D3B}.png)

![alt text](images/{2FF2F570-E271-420C-B0BA-D0C8542DA25A}.png)

## Server Component and Client Component

All the components by default are server components. To use Client Component, you can add the React `useClient` directive

Server Components run on server, not on browser

`Benefits of Server Components`

- Data Fetching
  - Server Components allow you to move data fetching to the server, closer to your data source. This can improve performance by reducing time it takes to fetch data needed for rendering, and te amount of requests the client need to make.
- Security
- Caching
- Bundle Size

![alt text](images/{53EB269E-D013-47C1-8AA0-054F7174FB2A}.png)

![alt text](images/{16DC6E9A-B8D3-4959-8821-8F2BE18B0985}.png)

![alt text](images/{1283D2D9-19E1-4303-BE4F-C7AEDD5AA1B8}.png)

`use Client;`

![alt text](images/{65F107A9-1619-4B57-8781-3C2255093E1E}.png)

`Server Component`

![alt text](images/{294582D0-CBE3-4CD5-A109-0072F8C4512F}.png)

![alt text](images/{8163912A-07EC-4543-8C34-274CC5079A0D}.png)

The above navigation link automatically direct to the `page.tsx` return in `app/counter`.
The name of the Component does not matter in this case. It only render whatever component returned as default from the `app/counter/page.tsx`

## Fetching Data

- Create a Server Component tours/page.tsx
- Just add async and start using await
- The same for DB

```
type Tour = {
    id: string;
    name: string;
    info: string;
    image: string;
    price: string;
}
```

```
const url = "https://www.course-api.com/react-tours-project"

async function TourPage(){
    const response = await fetch(url);
    const data: Tour[] = await response.json();
}

```

## Server Components are defined with async function

`app/tour/page.tsx`

![alt text](images/{C2DAD708-B6B9-491A-A5D5-8CFF16C89239}.png)

`components/Navbar.tsx`

![alt text](images/{E30C3BCA-C901-4A63-B187-061C6330610C}.png)

## <> vs `<section>` vs `<article>`

- <>...</> (React Fragment)

  This is not HTML. It's a React feature called a Fragment.

- <section>

  Represents a thematic grouping of content, usually with a heading.

  ```
  <section>
      <h2>Features</h2>
      <p>Our product offers...</p>
  </section>
  ```

- <article>

  Represents a self-contained piece of content that could stand on its own.

  ```
  <section>
    <h1>Latest Posts</h1>

    <article>
      <h2>Post 1</h2>
      <p>Content...</p>
    </article>

    <article>
      <h2>Post 2</h2>
      <p>Content...</p>
    </article>
  </section>
  ```

## Loading Component with the Server Component

If we define a loading component parallel to the Server Component it will render during the async call

![alt text](images/{E16D99D3-2FAE-46E5-B559-2560212DEBF1}.png)

## Nested Layout

We can have a nested layout, for all the pages within a Server Component in `app/`

- create `layout.tsx` file in the `/app/<NodeJS Componeent>/layout.tsx`

![alt text](images/{AE8564AC-9A2A-4554-8C51-D48D9329101F}.png)

## Dynamic Page

| Folder/file  | Meaning                       |
| ------------ | ----------------------------- |
| `page.tsx`   | Creates a route               |
| `layout.tsx` | Shared UI around child routes |
| `[id]`       | Dynamic segment               |

## Next Link Component

```
<Link href=''>..</Link>

```

## Files Structure

In Next.js App Router, files such as:

```
app/
├── layout.tsx
├── page.tsx
├── error.tsx
├── loading.tsx
└── not-found.tsx
```

must have those exact lowercase names because Next.js uses them as special convention-based files.

Next.js scans the filesystem and looks specifically for:

- page.tsx
- layout.tsx
- loading.tsx
- error.tsx
- not-found.tsx

If you rename them:

```
Layout.tsx
Page.tsx
Error.tsx
```

Next.js will not recognize them as route files.

`What about the component inside?`

The file name is lowercase, but the React component should be PascalCase:

| Type                        | File Name                                   |
| --------------------------- | ------------------------------------------- |
| Next.js special route files | `page.tsx`, `layout.tsx`, `error.tsx`       |
| Your React component files  | `TourCard.tsx`, `Navbar.tsx`, `Sidebar.tsx` |
| Hooks                       | `useTours.ts`, `useAuth.ts`                 |

## Next Images Component

![alt text](images/{A0878456-FEF7-47BE-9003-5796C4218C23}.png)

To use remote image in your application you need to add the domain name in the
`next.config.ts`

![alt text](images/{380BB362-BAAA-417E-BB98-9A5CDF68BF39}.png)

## More Route Options

![alt text](images/{EAAA3784-F181-4641-B7FA-419221499D74}.png)

# Server Actions

![alt text](images/{6B9AC97B-9CFB-46A8-8ECB-9C0D85A5BD20}.png)

![alt text](images/{00E396B6-AC8E-4E7A-87A4-9F13EB95FA46}.png)

![alt text](images/{BE7E900B-B9D1-4433-870F-A53DB773F521}.png)

## Revalidate Cache

In some scenarios, when we save data, the page does not reflect the new data, this happens becasue NextjS, cache the data in some scenarios like form etc. Whenever you face such issue use one of theese options

- revalidate cache
- redirect

```
import {revalidatePath} from 'next/cache'


saveUser(newUser);
revalidatePath("/action");
```

![alt text](images/{26B1F5A1-E380-4824-8528-23E6A37B8A3E}.png)

OR

![alt text](images/{42B133F5-600B-4188-9770-098BA35496A5}.png)

## useFormStatus

![alt text](images/{F1143774-C680-488F-9247-C67AD2B12D3F}.png)

![alt text](images/{309BE72C-3C92-42F0-B4C3-CE856083F080}.png)
