## What is React

- React is a javascript library for builing UI (User Interfaces)
- Developed and maintained by Facebook (now Meta)
- Source (https://www.udemy.com/course/react-tutorial-and-projects-course)
- Other competetors - Angular, Vue
- React is all about components
  - Component Benetifs
    - Independent, Isolated and Reusable User Interfaces, so change in one component would not break you entire app.
    - Easy to make changes, as just identify the component and make changes
    - **Virutal DOM**, Not whole application is rerendered, on a component updated. Which results into better speed and user experience

## Pre-requisites

- HTML
- CSS
- JavaScript (As React is a JavaScript Library)

## Development Environment Setup

- Browser
  - Google Chrome
    - React Developer Tool Extension
- Text Editor
  - VS Code
- Node.js
  - An open-source , cross-platform JavaScript runtime platform
  - Allow us to run JavaScript outside Browser and Develop fast.

  How to check is node.js installed or not?

  ```
  node --version

  v24.13.1
  ```

  - Always recommended to install LTS version instead of current version. Less chances of bugs.

## Create-React-App

- Used to create project structure with a sample react application, that helps us develop fast.
- More better option is **Vite** (Veet)

```
npx create-react-app frontend-app
```

- When we install Node.js, it also install **NPM** (Node Package Manager)
- NPM allow use to quickly install/update/remove external JavaScript Packages.

  ```
  npm install <package-name>

  npm i <package-name>
  ```

## Folder Structure

- node_modules
  - Node packages sepecified in package.json
  - Child Dependencies
    ```
    npm clean
    ```
- public
  - index.html
    - div "root"
  - Fevicon.ico
  - logo.png
- src (brain of the application - contain all .js, .ts, .jsx files)
  - index.css
  - index.js
  - components
    - NavBar.jsx
- package.json
  - name
  - version
  - dependencies
    - Node packages
  - scripts
    - start
    - build
    - test

      ```
      npm run <start/test/build>

      npm run start = npm start
      ```

  - eslintConfig

- package-lock.json
  - A Snapshot of entire dependency tree.
- Readme.md
  - Project Overview
  - Project Structure
  - Build Instructions
  - Architecture Diagram
- .gitignore
  - /node_modules (Huge in Size)

## Remove Boiler Plate Code

- remove src folder
- create src folder
- create index.js with base component

## First Component

```
function Greeting(){
  return <h1>Greeting</h1>
}

export default Greeting
```

Corresponding Arrow Function

```
Const Greeting = () => {
  return <h1>Greeting</h1>
}

export default Greeting
```

## Difference between JS function and React Component

- Starts with Capital letter
- Must return JSX (HTML)
- Always Closing Tags.
- Export it (export default Greeting)

In react you can many component but only one root component that contains all the other components.

## Extensions and settings

- Auto rename tags
- Highlight matching tags
- ES7 + React/Redux-Native Snippets (dsznajder)
  - rafce (An Arrow function with export)
  - rfce (An regular function with export)

    ![alt text](images/{056EED7D-D4B6-429B-B2B0-A5E5528A8EC0}.png)

  - Auto import react at top

    ![alt text](images/{96B8061D-80CF-48A6-8EDB-65360D95465B}.png)
    - prior to react 17 we had to import react at top, but no longer now.

      ![alt text](images/{F984F5A5-F0C0-4C61-B841-73508666E8EA}.png)

- Prettier - Code Formatter
  - settings

    ![alt text](images/{29243F03-0CFE-4B0D-B952-51110C35FEAC}.png)

  - Format on save/paste

    ![alt text](images/{1416C3A1-08E9-4EE2-9549-4413340C8E84}.png)

  - Default Formatter : Prettier

    ![alt text](images/{D30B5E43-3BF4-4D92-9E1C-B08E9274210D}.png)

![alt text](images/{429723C3-71C2-477C-804A-0D9E44C37E31}.png)

## JSX Rules

- Always return a single element
  - < section ></ section >
  - < article >< article />
  - Fragments : <></> (no unecessary div parent element in the DOM)
- Use camelCase for property names, to distiguish it from the native HTML properties, which are all lowercase.
  - onClick
  - htmlFor
  - readOnly
- In JSX, Use `className` prop to assign a `CSS Class` instead of `class` (which is a JavaScript reserved keyword)

  ![alt text](images/{68F2993B-166B-4B01-BE47-7285EBE8B4FE}.png)

- All tags have closing tags (In native HTLM we have few exceptions like img)
- Use paranthesis while returning

  ![alt text](images/{293BC8EA-117F-4668-AFFE-92E25EBA8AD7}.png)

## Nested Components

![alt text](images/{17E1CB6B-4898-4D20-9F32-660BB31D607F}.png)

## React Developer Tool

This extension allow us to inpect and debug the component hierarchy, state and props in the react appication.

![alt text](images/{9C4EEEB5-0148-48BC-BB33-BE691B10E6ED}.png)

![alt text](images/{66ABEF73-F75B-4BF7-AE75-BBA5CBE4E3E1}.png)

![alt text](images/{7863D1CD-66F2-481E-94DE-85E029E8C3C3}.png)

![alt text](images/{80FAE5FB-54A7-4353-B9A6-F33A678CE7A7}.png)

Very useful in understanding and debugging components

React Component is defined as a JS Function, but is used in JSX like `<Book\>` not Book(),

## Advantage of Components based Architecture

- Moduler codebase
- Easy to maintain
- Reusability

## Local Images (public folder)

- Remote Images
  - hosted outside like on S3 Buckets or Azure Storage Account
  - Use URL to the Image
- Local Images
  - In public folder
    - Not a best practice
    - less performant
  - In src folder
    - Prefred approach
    - under the hood they are optimized
  - replace `url` with `relative path` for image `src`

    ![alt text](images/{25075372-B377-4FBA-9A1B-1B391E781022}.png)

## JSX - CSS

**Inline CSS**
In JSX, for inline CSS, use `style` prop, which uses a JavaScript Object Format, enable dynamic styling.

The value should be an object with camelCase property names.

```
return (
  <>
    <div style={
      ## JS Object
      {
        background :'red',
        color : 'black'
      }
    }>



    </div>
  </>
)
```

We can also change CSS dynamially like for profile using inline style CSS

![alt text](images/{406AB210-E165-4538-9663-03EFCD95165C}.png)

**External CSS**

```
.book {
  background : red,
  color : black
}
```

```
return (
  <>
    <div className="book">



    </div>
  </>
)

```

## JSX - JavaScript

- {} in JSX can contains JavaScript expression returning a value
- { expression } - Must return some value
- { \` # {expression} \` } : For string with expression

```
const title = "Title Text"
const heading = "This is the heading";

const randomMessage = (message) => {
  if(message.length() > 0)
    return "I have title";
  else
    return "I have no title";
}

return (
  <>
    <img src={img} alt={title}>
    <h1>{heading}</h1>
    <h2>{randomMessage(heading)}</h2>
  </img>
)

```

This is very useful in making reusable components, where the value come either from inside the component or from outside via props

We can use JavaScript function on string values

- message.toUpperCase()
- message.toCamelCase()
- message.toLowerCase()

![alt text](images/{7CCA52BF-BAF1-4989-BDE7-E42B934D3CAF}.png)

The downside of this appraoch is that the `Book` Component is private and can't be reused with other components.

## Arguments and Paramters

In vanila JavaScript,

- `Parameters` are defined while defining a function
- `Arguments` are actual values , passed during calling that function.

In React, we pass values to the Componets using `Props`

![alt text](images/{B5DB956C-9986-417C-AAF3-09B0312D3FCB}.png)

![alt text](images/{BC977760-6B28-4AC0-A13C-2BDBD8EDBBBA}.png)

If you dont provide the props values, either you will be displayed nothing or get an error.

## Dynamic Parameters

![alt text](images/{83DFEE94-05A5-4C40-8344-3AFA310E400A}.png)

## Props Multiple Approaches

```
const book1 = {
  title : 'Atomic Habits',
  description : "Self Improvement",
  price : 130
}


// Option 1
return (
  <Book
    title={book1.title}
    description={book.description}
    price={book1.price}
  />
)

// Option 2
const {title, description, price} = book1;

return (
  <Book
    title={title}
    description={description}
    price={price}
  />
)

```

Similary with `Props`, a JS Object

![alt text](images/{A017FD2A-740D-41D8-BFA5-F37AC7C10667}.png)

will be replaced with

![alt text](images/{C7BBD2F4-FC54-468E-9F02-2B5975A541F8}.png)

Alternate Approach

![alt text](images/{047D11AC-E02A-4F57-BA08-AFA7F54F57C4}.png)

Downside, you can't log `props` within the component.

## Children Props

- Everything we rendered within the Component tags
- Used while setting up Context API

![alt text](images/{D481302B-5344-4785-A655-8371EC960DE4}.png)

![alt text](images/{6DF7591E-6FCA-45DF-B046-2FA97E63F025}.png)

`children` is a keyword, cant use any name.

So if we have multiple books, few have children, few not. Used in those scenarios.

## Array of Objects in React

- In React we use JavaScript `map()` method to workd with Arrays

- `map(item, key)` create a new array by calling a function for every array element

- key must be unique, any property, and can be of any datatype string, number.

- `key` is used while we render list using `map(item,key)`

- key prop is very important,while rendering a list of items in React, because React uses the key prop to uniquely identify a list item and update only the changed element during rerendering (Optimized updates and rendering)

- Avoid using `key={index}`, rather use `key={book.id}` as using index as a key can cause issue, when reorder or modify the items, as the React rely on unique keys for stable updates.

- Here the `number` prop passes the current index from the books array to the `Book` component

- If you map over an array in JSX without providing a key prop, React will throw a warning in the console about missing keys.

```
<Book {...book} key={book.id} number={index} />;
```

```
const newArray = oldArray.map((item,key) => {
  console.log(item);
  return <p>{item}</p>
})
```

```
const booksArray = [
  {
    id : 1,
    title : 'Atomic Habits',
    description : 'Self Improvement',
    price : 145
  },
  {
    id : 2,
    title : 'Stop Worring & Start Living',
    description : 'Motivational',
    price : 350
  }
];

```

**First Approach**

![alt text](images/{1A2C38B6-1292-4B80-8894-13B45F05D60A}.png)

**Second Approach**

when we have smaller JSON object, But in case we have big JSON object, there is an alternate approach is, pass the entire JSON as props.

![alt text](images/{8514032F-3074-4087-A4EA-FF41AF9363B8}.png)

**Third Approach**

Use spread operator

- const newFriendsList = [...friends, newFriend] copy values

```
const book = {
  id: 1,
  title: "Harry Potter",
  author: "J.K. Rowling",
  image: "cover.jpg"
};

Then

<Book {...book} key={book.id} />

Equivalent to

<Book
  id={book.id}
  title={book.title}
  author={book.author}
  image={book.image}
  key={book.id}
/>
```

so `{...book}` flattens the book Object and pass each property like (id,title,author,image) as a separate prop to the Book component

![alt text](images/{4438B7EF-792E-44D9-9A6B-FFB4C52332A1}.png)

[For Arrys]

![alt text](images/{FAF3EBA8-FB8B-44EF-8829-FEF97CDF8037}.png)

[For Object]

![alt text](images/{3FAF5DF7-A667-4BFF-9685-FD774384A6B3}.png)

## Events Basics

[In JavaScript]

```
const btn = document.getElementById("submitBtn");

// Callback function
btn.addEventListener("click", function(e){
    // Access event object e
    // Do Operations
    // ----
})
```

[In React]

```
const handleButtenClickEvent = (e) => {
    // Prevent the default action of the form submission, that is refreshing the page.
    e.preventDefault();

    // Event object properties
    e.target.name
    e.target.value (Input value)

    // Do Operations
    // ----
}

<Book onClick={handleButtenClickEvent} />

```

**Different Events**

- onClick (Click Event)
- onSubmit (Submit Form)
- onChange (Input Change)

## Form Submission.

- Define a `const handleSubmit = (e) => {..}`
- Specify `<form onSubmit={handleSubmit}` at Form
- Include `<button type="submit"/>`

![alt text](images/{965F0B25-9E2F-4CE2-B4B8-5BE7ACDF3071}.png)

![alt text](images/{5BB4AA64-EC5D-4CB3-9F41-60E90FADFACC}.png)

## Anonymous Function

Anonymous function only executes when the button is clicked or when the event occur

![alt text](images/{22AD3A6A-B6DF-437A-9100-61077E5B01C6}.png)

## Components Feature

Components are independent by default.

So the function can access to props, specific to that component.

![alt text](images/{7751A5E2-9CF1-4F17-BBC1-4B1C3E2ED7F4}.png)

## Prop Drilling

In react we can only pass the data down (Parent -> Child), not otherway around. So you have to pass the data through each componennt in between the source to destination component. This process is called `Prop Drilling`

Alternative to this are

- Context API
- Redux
- Other state liberary

## Get a particular item from the list

![alt text](images/{504D9589-EB52-4631-8747-D796F86B0B68}.png)

![alt text](images/{0DA80666-AB56-46ED-BC03-364F735300A5}.png)

`onClick={getBook(id)}` will not work, here it invokes the getBookId function immediately when the component renders, rather on button click.

[FIX A] : Use Function reference

![alt text](images/{1DB67E68-AF32-4341-94D3-AE94C5DEE30C}.png)

[FIX B] : Use Anonymous Function

![alt text](images/{C9862259-E314-461B-BFED-A79054DF48B4}.png)

```
const findBook(id) {
  const book = books.find( (book) => book.id === id);
  return book;
}
```

## ES6 Modules

- `Named Export` : Name in `{book}` during import must match the export name

  ![alt text](images/{A6677F79-D436-49A2-8186-E68DB37F73B0}.png)

  ![alt text](images/{09BB7AA6-F21E-470D-9EB9-1B6B8E67F696}.png)

- `Default Export`: Only one default export per file. Name during import can be differnet and no `{}`

  ![alt text](images/{4CE05E49-64B4-4973-978C-61681399D6D4}.png)

  ![alt text](images/{948BF222-2234-435C-822A-0568BF4B76E0}.png)

## Local Images (src folder )

![alt text](images/{59619AEF-21E0-4C8D-B2FB-366E434C0D2A}.png)

## JSX - String + expression

```
<h1>`string ${expression}`</h1>
```

JavaScript

```
const number = 5;

<h1>{number + 1}</h1>
```

HTML Output

```
<h1>6</h1>
```

JSX

```
const number = 5;

<h1>`Euro {number + 1}`</h1>

<h1>{`Euro ${number + 1}`}</h1>
```

HTML Output

```
<h1>Euro number + 1</h1>

<h1>Euro 6</h1>

```

## Build Folder

When we deploy the application, we need to build the application and put it into a specific format to be deplyed, as it will not follow the development setup format with

- src
- public
- node_modules
- ..
- ..

**package.json** has scripts

![alt text](images/{E6B20005-D49B-442C-A44F-3953EC335EB8}.png)

The below command will do the job and create deployable version of the application into a new folder `build`

So `build` folder contains the production ready application

```
npm run build
```

![alt text](images/{2A71BD10-88B5-4AE5-8A4F-711FE54C288A}.png)

## Deployment

Deploy it onto platforms :

- Azure
- AWS
- Netlify

## Why is StrictMode included in a React project by default?

StrictMode is included by default in many React project templates (such as those created with older versions of Create React App and some Vite templates) because it helps developers find potential problems early during development.

```
import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
```

It enables additional development-time checks and warnings, such as:

- Warning about deprecated APIs
- Highlighting issues that may cause bugs in future React versions
- Identifying unexpected side effects
- Encouraging reusable and resilient components

StrictMode:

- ✅ Runs only in development
- ✅ Does not affect production behavior
- ✅ Helps detect bugs and unsafe patterns
- ✅ Is included by default to encourage better React code quality and future compatibility

When you run:

```
npm run dev

or

npm start
```

your tooling loads React's development build.

This build includes:

- StrictMode checks
- Extra warnings
- Detailed error messages
- Development-only validations

When you run

```
npm run build
```

the creates an optimized production bundle and uses React's production build.

This build removes:

- StrictMode double-invocation behavior
- Most warning messages
- Extra validation code

![alt text](images/{5B6C3D30-18DC-4481-8923-DC0403A3FE2D}.png)

- Only comes with `create-react-app`, not with `vite`

## How can you pass the entire book object as a prop in a React component?

Use the spread operator `{...Book}` to pass all properties at once.

## In React, why is using camelCase important for attribute names in JSX

Because JSX attributes map directly to JavaScript properties.

JSX attributes like className correspond to JavaScript DOM properties.

## JSX Comments

```
{/*  ---  */}
```

![alt text](images/{AB2CFB6E-F46C-4E08-A5BF-23FD44DDEC76}.png)

## Vite

- Faster than create-react-app
- Out-of-the-box support for TypeScript, JSX, CSS

## Vite Commands

```
npm create vite@latest <app-name> -- --template <template-name>
```

![alt text](images/{27E00EC9-ADD1-4722-9F54-5412CECE6EBD}.png)

[package.json]

![alt text](images/{1B27090E-B61C-478D-83B8-4C8FF200553C}.png)

```
npm install

npm run dev       (http://localhost:5173, where create-react-app uses http://localhost:3000)


npm run build
```

With Vite you need to use:

- Need to use .jsx or .tsx extension
- index.html is in the `src` folder, instead of `public`
- fevicon.ico and logo.png still in the `public` folder
- instead of `index.js`, it uses `main.jsx` or `main.tsx`
- to spin up dev server `npm run dev` instead of `npm start`
- `npm run build` create `dist` folder instead of `build` folder for bundling production ready application.

## Advance React Topics

- Hooks
  - useState
  - useEffect
  - useRef
  - useContext
  - useReducer
- Forms
  - controlled/uncontrolled inputs, values,
  - onChange
  - FormData API
- Other Topics
  - Context API
  - Prop Drilling
  - Custom Hooks
  - Performance

## useState Hook

`Problem Statement`

The onClick increasing the count, but not updating it in the UI

![alt text](images/{36535B3E-6982-4ADD-8846-07DC69E40C3F}.png)

`Root Cause`

We are not triggering the rerender of ReactJS component.

So the vanila JS is working fine, but not React.

`Fix`

If we want to see the latest changes in the browser in React, we need to perform 2 tasks:

1. Set a state value
2. Trigger Rerender.

## Set state value using useState Hook

- Import it using Named import

  ```
  import {useState} from 'react'
  ```

- useState() is a function that returns an Array with two values
  - state value (preserved with rerenders)
  - function

  ```
  console.log(useState());
  ```

  ![alt text](images/{A27ED69B-A15C-420A-A0F3-377EBDC8B43A}.png)

  ```
  console.log(useState('bob'));
  ```

  ![alt text](images/{853602B6-DEC0-44FB-A19F-3C573A9D9655}.png)

  ```
  console.log(useState(1));
  ```

  ![alt text](images/{3F3D37AE-F9C5-4AF9-91F3-06F0C9EB2981}.png)

  ```
  const value = useState('hello')[0];
  console.log(value);
  ```

  ![alt text](images/{B7C63367-39E7-4E81-957D-9314A76455B8}.png)

  ```
  const func = useState('hello')[1];
  console.log(func);
  ```

  This print the default function, that control that value.

  ```
  const [count,setCount] = useState(0)

  ```

  ![alt text](images/{8E6380DA-FEED-4D69-854F-3F324CDE05B4}.png)

  ![alt text](images/{FCABBA4E-AED4-47C9-B15A-A498D0E11FAF}.png)

## Initial Render and Rerender

- Initial Render happens when the application first loads or when the root component is first rendered. This is also called mounting the components

- Rerender happens when the component's state or props change, and component need to be updated in the DOM, to reflect the changes. React uses Virtual DOM to optimize the process of updating the Actual DOM, so that only necessary changes are made.

- We no longer need to import "react"

  ```
  No Longer needed..

  import React from 'react'
  ```

## General rules for Hooks

- Name start with `use` for both build-in and custom hooks
- Invoked inside the Component body, not outside the Component body
- Hooks does not work inside conditions like if-else.
- Set function does not update state immediately. So don't expect synchronous behaviour.

## useState with Arrays

1. Import Data

   ```
   import {data} from './data'
   ```

   ```
   [./data.js]

   export const data = [
    {
      name: 'ram',
      age: 10
    },
    {
      name: 'sham',
      age: 20
    }
   ]
   ```

2. Set Initial State

   ```
   import {useState} from 'react'
   .
   .

   const [customers,setCustomers] = useState(data)

   ```

3. Render the Array items

   ```
    .
    .
    .

   return (
      <>
        {
          costomers.map( (customer,id) => {
                                        const {id,name} = customer;

                                        return (
                                          <div key={id}>
                                            <h1>{customer.name}<h1>
                                            <button>Clear All</button>
                                            <button>Delete Item</button>
                                          </div>
                                        );
                                      }
                        )
        }
      </div>
   );

   ```

4. ClearAll and RemoveItem functionality

   ```

   const clearAll = () => {
    setCustomers([])
   }


   const removeItem = (id) => {
    const newArray = customers.filter((customers) => customer.id !== id);

    setCustomers(newArray);
   }

   ```

5. Attach the ClearAll and RemoteItem functions to the HTML

   ```
   <button onClick={clearAll}>Clear All</button>

   <button onClick={() => remoteItem(id)}>Delete Item</button>

   ```

   ![alt text](images/{878E077E-FAAB-45B4-BC04-C8211CF2B90E}.png)

## useState Optional or Mandatory

- useState is quite useful, if you are adding/updating/deleting the items, if you are just rendering, then you donot need useState. But as a best practice to use useState, so in the future you can add functionality.

  ![alt text](images/{CD18F8BC-E81D-47DC-89C1-E417BE135297}.png)

## Auto batching

![alt text](images/{A30384A5-50A0-4F7B-BA04-0E6AF3D3BA90}.png)

The below will perform only 1 rerender not 3 rerenders

![alt text](images/{54119FE1-7740-4E87-BD1D-E9C50B41DBFB}.png)

## useState with JS Object

![alt text](images/{392DA90B-799B-42F2-9FA0-EC5302C1685A}.png)

![alt text](images/{19558B52-91EC-4C0C-A12F-6F2562BAF766}.png)

## useState is not synchronous

See the console.log still prining the old value.

![alt text](images/{047D11AC-E02A-4F57-BA08-AFA7F54F57C4}.png{D6428207-E019-4D6A-A412-081AD6A09614}.png)

## useState function update approach

Fix to the synch issue.

![alt text](images/{8D66E6C1-472F-432F-8E57-C1B322C3B269}.png)

![alt text](images/{CDB257EE-1304-4449-932E-FC44EA913909}.png)

## useEffect Hook

`Problem Statement`

![alt text](images/{C922E447-2FB3-4D6E-B76F-3C834F91CED3}.png)

The sayHello() invoked on every rerender. So everytime component is rerendered, react create the function, and if there is invocation as shown it will invoke.

So this might cause issue if seyHello function has code that update the state, in that case it will get into infinite loop.

![alt text](images/{E0835E91-8108-49B2-9174-E51B23C9E231}.png)

`Fix`

useEffect Hook helps us to invoke code conditionally.

![alt text](images/{3D032486-E069-489C-8041-CEEA8B95D095}.png)

So in useEffect, the callback function will be invoked on every rerender by default

![alt text](images/{6AFB3C36-CCE4-42FF-BCBB-34D4E340A444}.png)

But if we pass the second argument as [], it will be invoked only on initial render

![alt text](images/{9291E585-F690-4D0D-A96E-193704DE9AC9}.png)

useEffect can not use async callback function, as async function return promise

`This is not allowed`

![alt text](images/{9FB13B54-2B99-4D34-864A-D444BB94AE0C}.png)

`But invocing async function inside the callback function is OK`

![alt text](images/{34A0DEC0-AFA6-4C4E-A177-05F01A42B1BB}.png)

## useEffect - multiple Effects

How we can have multiple effects in our application using depdendency array.

The below useEffect make the callback function invoke only once, during initial render.

![alt text](images/{41AC182A-CD0E-457D-AEDE-3709A40D38F4}.png)

Here below the first useEffect will be invoked on updated of firstValue and SecondValue

![alt text](images/{73D8F109-C16C-471A-ACB3-EBEA64C3564E}.png)

Here below the first useEffect will be invoked on updated of firstValue and second useEffect will be invoked on SecondValue update.

![alt text](images/{11C87E85-E771-4F2C-9A45-92AEDC8C038D}.png)

## useEffect with Fetch API

- fetch API, does not catch 404 error, so on exception, the catch block will be skipped.

Here the error is from the first log inside try.

![alt text](images/{7EDC2C34-A0B7-4120-8022-93C1319EC734}.png)

To check the JS object properties, log the object

![alt text](images/{9978E858-8982-4050-8B1F-2C9760BEEFAE}.png)

![alt text](images/{342F2134-6BCA-4875-84E2-9C2E6D67A3AF}.png)

## What will happen if you call setState directly within a function outside useEffect

It will cause an infite render loop if the function is invoked during render.

## What happens when the dependency array of a useEffect is left empty ([])

The effect runs only once during the component's initial render.

## Multiple Returns in React Component

- JS reads from top to bottom. so make sure you load data before returning JSX

- useState is used for
  - defining state variable with initial value
  - defing the function for update the state
  - Components using the state variable will automatically rerender. (`Biggest Advantage`)

- useEffect is used in two scenrios
  - Load external data via fetch API, before the component return JSX.
  - invoke a function on state value update

- Component
  1. Define the state variable using useState
     ![alt text](images/{0B5ED73E-651C-438C-BFC0-57E14B7E179F}.png)
  2. Defind the useEffects for
     - Loading external data

       ![alt text](images/{6F5EEEE9-1695-473D-8269-176C22ABB593}.png)

  3. Multiple Return JSX based on state value

     ![alt text](images/{22AED4EF-1FB9-4B44-8982-8032ED0D2688}.png)

## Fetch skips 4xx or 5xx error - Work around

![alt text]({EF54024E-AAC9-448D-AD29-41A60BF9013E}.png)

![alt text](images/{66F5547F-21EE-40F1-B5D1-406230F6E4F0}.png)

## Order Matters

Always make sure you put the error returns first before successful fetch return.

![alt text](images/{9070D31C-29AF-4D87-A7A6-9CB1C4877A62}.png)

## Avoid putting function outside the useEffect

Avoid below approach as it causes multiple issues

![alt text](images/{E757B537-3B51-4C80-A247-52488B1146E2}.png)

## React Hook rules

- Never put hooks in condition like if-else
- always put hooks before functions return, else will be skipped

![alt text](images/{D27AB303-FC57-412A-AEEF-1E2A9EE566B3}.png)

## What is the purpose of multiple returns in a React Component

To handle conditional rendering scenarios like

- loading
- error
- success states

## What happend if you destructure a property from a null object in a React Component

JavaScript throws an error, breakinig the application

## When using fetch API, why is it important to check the resp.ok property after making a request

To ensure the response status is successful (eg: 200 Ok)

## JS "truthy" / "falsy"

![alt text](images/{79FAAC95-B7F2-40BC-A3FD-1511921AD3E1}.png)

![alt text](images/{A0E9F76F-7324-4ED6-A132-032BBBD87DF0}.png)

![alt text](images/{7BB6F10B-F78B-4535-9FE0-22A0CFDB28F2}.png)

## Short Circuit

![alt text](images/{F6B07EBB-8317-4B30-A100-D238939F2ECB}.png)

## Ternary Operator

![alt text](images/{EEB70056-F873-4DBF-99AA-0B40F22C4005}.png)

## Toggle Alert

![alt text](images/{38C060AC-3FDF-486E-B4EE-02FE9AC0DE67}.png)

## User Challenge

![alt text](images/{A4FE0556-45B8-4934-B497-41F78721E582}.png)

```
import { useState } from "react";

type User = {
  name: string;
};

function UserChallenge() {
  const [user, setUser] = useState<User | null>(null);

  const login = () => {
    setUser({ name: "varinder" });
  };

  const logOut = () => {
    setUser(null);
  };

  return (
    <>
      {user ? (
        <>
          <h4>Hello there {user.name}</h4>
          <button onClick={logOut}>LogOut</button>
        </>
      ) : (
        <>
          <h4>Please Login </h4>
          <button onClick={login}>Login</button>
        </>
      )}
    </>
  );
}

export default UserChallenge;

```

## useEffect Function in a child component with condition

If you render the child component conditionally, the useEffect even with empty dependency array will be executed everytime it is render and rerendered. As the component mounts and unmounts.

![alt text](images/{92CEDAD2-2BB4-4F5A-A1B4-F3A74F2D9F9D}.png)

```
import { useState } from "react";

function UserChallenge() {
  const [toggle, setToggle] = useState(false);

  const handleToggle = () => {
    setToggle(!toggle);
  };

  return (
    <>
      <button className="btn" onClick={handleToggle}>
        Click Toggle
      </button>

      {/* Ternary Operate always inside { } */}

      {toggle ? <RandomComponent /> : <></>}

      {toggle && <RandomComponent />}
    </>
  );
}

const RandomComponent = () => {
  return <h1>Hello There</h1>;
};

export default UserChallenge;
```

## Project Structure - Folder

`components/Navbar`

![alt text](images/{B41ED8C8-3195-4ED5-9D40-997758C40EA1}.png)

`index.jsx`

![alt text](images/{B3EC5593-CB84-46A5-BE18-C5A3AFBD4B53}.png)

## Project Structure - Named Export

`ìndex.jsx`

![alt text](images/{9C5F34B4-B6D4-4009-87E5-775EE67D70D3}.png)

`App.jsx`

![alt text](images/{EA85BDB1-BD2A-4799-B866-9DD941B87594}.png)

## JSON Parsing

![alt text](images/{995FACAA-DD47-4A44-B8CB-BB2B1DD32DD0}.png)

![alt text](images/{22F51ED3-9AA5-4D26-8A68-211E05BA0D15}.png)

With the map() => { return } - return JSX is mandatory

## || vs ??

const img = image?.[0]?.url || "default Image"

const img = image?.[0]?.url ?? "default Image";

```

const url = "";
console.log(url ?? "default Image");
// ""

const url = 0;
console.log(url ?? "default Image");
// 0

const url = null;
console.log(url ?? "default Image");
// "default Image"

const url = undefined;
console.log(url ?? "default Image");
// "default Image"
```

# What is the purpose of the optional chaining operator (?.) in JavaScript?

![alt text](images/{363311AD-8358-4BAD-99DB-0CDC31760185}.png)

It prevents errors when accessing properties of null or undefined objects by safely returning undefined without throwing error. (?.) checks if the object or property exists before trying to access it.

## In the Person component, what is the fallback value for the nickName prop

![alt text](images/{6075637A-837C-4614-9741-CC8382D384CD}.png)

shakeAndBake

## In the Person component, what happens if the images array is undefined?

![alt text](images/{680F31CD-DF65-47CF-AA5F-0EB80AFA71EF}.png)

An Avatar image is displayed

## Forms in React

![alt text](images/{4F6F1D0B-F539-4AB0-BC16-0D22F6883309}.png)

## Controlled inputs

![alt text](images/{78CD7DFC-7D86-4D8C-98A8-DFF0C5B1562B}.png)

## User Challenge

![alt text](images/{A857D7FF-B71D-4C94-BEE7-F24C42B05E7B}.png)

```
import { useState, useEffect } from "react";
import { UsersData } from "./UsersData";

function UserChallenge() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [users, setUsers] = useState(UsersData);

  // Fix : React State Sync Issue
  useEffect(() => {
    console.log(email, password);
  }, [email, password]);

  // Handle Submit
  const handleSubmit = (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

    setEmail(e.currentTarget.email.value);
    setPassword(e.currentTarget.password.value);

    const newuser = {
      id: Date.now(),
      name: e.currentTarget.email.value,
      age: 10,
    };

    setUsers([...users, newuser]);
  };

  // Handle Remove
  const handleRemove = (id: Number) => {
    const newArray = users.filter((user) => user.id !== id);
    setUsers(newArray);
  };

  return (
    <>
      <div className="container">
        <form className="form" onSubmit={handleSubmit}>
          <div className="form-row">
            <label className="form-label" htmlFor="email">
              Email
            </label>
            <input className="form-input" type="text" id="email"></input>
          </div>

          <div className="form-row">
            <label className="form-label" htmlFor="password">
              Password
            </label>
            <input className="form-input" type="password" id="password"></input>
          </div>
          <button type="submit">Submit</button>
        </form>

        <h3>List users</h3>
        {users.map((user) => {
          const { id, name, age } = user;
          return (
            <div key={user.id}>
              <p>{user.name}</p>
              <button onClick={() => handleRemove(id)}>Remote</button>
            </div>
          );
        })}
      </div>
    </>
  );
}

export default UserChallenge;
```

## Multiple Inputs

1. Define the intitial state as Object intead,

   ![alt text](images/{ECDAA25C-5BDB-49AD-AD20-828934B6B186}.png)

2. Define onChange={handleChange} with each Fields has `name` property

   ![alt text](images/{164BFECC-9654-4326-8D9E-5C1119B174BE}.png)

   The `JSON property` and `HTML name ` much be same

3. Add HTML with `name` and `onChange={handleChange}`

   ![alt text](images/{3A21D7FC-7CDF-4672-B4AA-87EC0A6FCD27}.png)

4. Define the `onSubmit={handleSubmit}` at form level

   ![alt text](images/{4DABB83B-A710-4F06-99D3-D4FA7743FE42}.png)

## Checkbox Input

1. Define the state variable

   ![alt text](images/{CEC0EFA9-5AD3-4628-BB7E-F5BC49D3FFDE}.png)

2. Define the HTML Element

   ![alt text](images/{7B4569D2-07D6-416E-B0FD-E867250D747B}.png)

3. Define the handler function

   ![alt text](images/{563979A8-0F52-4F14-9BC5-1593871BB6AB}.png)

## Select input

1. Define the state variables

   ![alt text](images/{AAECD95D-73B7-4222-86E7-AB6301418293}.png)

   ![alt text](images/{CD8134F0-2CD8-455D-8323-AACA5E776C47}.png)

2. Define the HTML Element

   ![alt text](images/{52E68901-3600-4DF4-9565-1E38FDCB7B17}.png)

3. Define the handler function

   ![alt text](images/{FA437654-6EA2-4485-9624-EDE82B56245D}.png)

## FormData API

If you working with form, make sure you have `name` property for all the HTML elements, along with `id` property

1. Define the `onSubmit={handleSubmit}` at form

   ![alt text](images/{59235B37-DF3A-4C6E-976B-B7C0EC5B868A}.png)

2. Access the form elements values using `FormData`

   ![alt text](images/{E2FD699E-B8D6-48C5-887E-B3971E8D670D}.png)

3. Clear the Form after submit

   ![alt text](images/{157CB09B-0A0A-4F96-B9CF-30E5AE53DFE9}.png)

## In the ControlledInputs component, what happens if you do not call e.preventDefault() in the handleSubmit function?

Without e.preventDefault(). it will trigger default browser behaviour, which is reload the page on form submit

## In the MultipleInputs component, what ensures that each input updates the correct property in the user state object

```
const handleChange = (e) => {
  setUser({ ...user, [e.target.name]: e.target.value });
};
```

Each HTML element must have `name` property.

The name attribute on each input allows the handleChange function dynamically update the corresponding property in the state object using [e.target.name: e.target.value]

## In the UncontrolledInputs component, how is the FormData API used to retrieve form data after submission?

```
  const formData = new FormData(e.currentTarget);
  const newUser = Object.fromEntries(formData);
  console.log(newUser);
```

The FormData Object create key-value pairs from input fields

## useRef Hook

Quite similar to useState Hook, persists state value.

The only difference is updating state with useRef, does not trigger rerender.

1. Define the state Variable

   ![alt text](images/{1158CF52-B1D7-413A-A55F-95261878BFFA}.png)

   ![alt text](images/{FB7650DE-E559-44A1-9284-A2E5AF518D78}.png)

   React save the useRef variable as an JS Object with `current` property value set to the default value.

   ![alt text](images/{101227E8-62DB-4787-9E19-EBBE7B52B51A}.png)

   ![alt text](images/{BC613BB3-7FB0-4D47-B30E-439F4449AB1D}.png)

2. Access the state value

   ![alt text](images/{C3584682-F03F-454F-BEBA-378E71654906}.png)

   ![alt text](images/{93025607-C93B-4D77-86C4-B3F6D5A67F70}.png)

## useRef - Initial Render

1.  Define a state variable and a ref variable

    ![alt text](images/{AB47FA37-D633-4662-9766-C62B6B9D7C93}.png)

    ![alt text](images/{DFFF4656-6DB8-4FFF-8ABA-73B398CD6C33}.png)

2.  Define a useEffect, with dependency array with state variable `value`, so it gets triggered whenever `value` state variable value is changed.

    ![alt text](images/{E4269AB0-343E-44A8-80CB-3BD1002AE1DE}.png)

    ![alt text](images/{6798973E-E2A7-475E-8713-025D0293021B}.png)

3.  Lets suppose you want to skill a function invocation at initialization and invoke afterwords whenever the state value changes, you can do as below

    ![alt text](images/{2FB53A1F-DA29-453B-8148-85028E468515}.png)

- During the page load, the `value` state variable is initialized.
- As useEffect has dependency on `value`, it will be invoked, the if condition passes and it set the Ref variable ´isMounted´ to true and return, without invoking the `console.log("re-render")`. So on initialization of `value` state variable, we skipped the `console.log("re-render")` execution using the Ref variable `isMounteed`. Afterwords it will be invoked on every `value` state change as `isMounteed` is set to true during initialization of `value`

We can think, why we can't use a simple variable, in that case we can't use `useRef{isMounted}` inside the HTML, to get the current value from an HTML element, without rerendering the HTML

We can think, why we can't use another state variable, in that case we can't skip the rerender, `useState{isMounted}` inside the HTML, will perform the rerender.

## How does the useRef hook assist in accessing DOM nodes in the example below?

```
const refContainer = useRef(null);

useEffect(() => {
  refContainer.current.focus();
});
```

useRef stores the reference of DOM Object in `current` property

## Custom Hook

A reusable functionality can be implemented as a custom hook, if it is being used in multiple components

![alt text](Images/{83EE0541-35B6-435B-8931-C673B5B7CD35}.png)

Let's suppose we want to use this toggle functionality in multiple components, so rather than implementing this in a single component, we can put this functionality in a separte file as a custom hook, and can use in multiple components.

Below are the steps

1. Put the reusable function into a separate file with its state variables and function

- Name must start with `use`
- Must return `{state`,`setState}`

  ![alt text](images/{AAB33BDE-89DA-466A-B6D1-CF96EA4FA9CA}.png)

2. Use the custom Hook

   ![alt text](images/{0ACEAAB6-9F82-458B-A318-AAB52E6286E6}.png)

   ![alt text](images/{1FB290CA-21B7-4C0F-AEA4-4AF63AB31CDE}.png)

   ![alt text](images/{EFE3AE46-2C92-4F27-92E4-7D2744CCB624}.png)

## Difference between React Component and Custom Hook

- React Component has its own state and return JSX
- Custom Hook has its own state and return `{state,setState}`

- React Compoent name must be UpperCase
- Custom Hook name starts with `use`Toggle

- React Component is used as HTML with props
- Custom Hook is used as a variable
  ```
  const {show,toggle} = useToggle(false)
  ```

## Create a custom Hook - Toggle

![alt text](images/{0E69EE78-E1C9-44B3-AA0F-FC1A2BE7F494}.png)

![alt text](images/{60B9CFAD-ED80-4B88-9345-A2AE7D14EEB8}.png)

![alt text](images/{246526FD-B6E7-4151-A7B2-BA17AFD66BF6}.png)

![alt text](images/{FB0662EF-26AE-4D6E-97DF-8E583F61F9FC}.png)

## Custom Hooks - Fetch User

![alt text](images/{A01F0342-E9D5-480B-A3C5-D4ACA19707B7}.png)

1. Define the Custom Hook

   ![alt text](images/{F5B97F84-4469-46A3-8CB2-017B49D79FF1}.png)

   ![alt text](images/{115B9D0B-6310-4D12-B219-D212DA4A6CB8}.png)

2. Use the Custom Hook

   ![alt text](images/{4D0FB86F-CE56-40DB-8CFF-6D9824D9EB9C}.png)

## Custom Hook - Generic Fetch

1. Create custom hook with generic name

   ![alt text]({37BFE1CB-46CB-4CD7-AF15-ED25F156A4B9}.png)

2. Use the custom hook

   ![alt text]({B719AC48-3093-429C-B6DD-BABC1DBAE10C}.png)

## What is the primary purpose of custom hooks in React?

To extract and encapsulate reusable logic, reduce duplication and simplify component code.

## Context API

It solves the Prop Drilling issue, As prop drilling becomes complicated when we have many components in a page.

`Problem Statement : Prop Drilling`

![alt text](images/{113C8A69-BAC4-4464-BF46-DBD819817FD9}.png)

![alt text](images/{8C5812BF-46FC-4ACA-98FE-FA3D46C3905B}.png)

![alt text](images/{9CB5A728-1122-4F03-BE48-B52A114CF3C9}.png)

`Solution - Context API`

1. import the context from 'react'

   ![alt text](images/{008C1EF7-902E-48A6-A602-74E0730A4149}.png)

2. Initialize and export the context, so can be imported in other components

   ![alt text](images/{47544259-B15F-4071-8400-581ED208E467}.png)

   ![alt text](images/{47C9CEF5-D6AC-4C8A-A252-C73F774B2F17}.png)

   The Context Object has Provider and Consumer

3. In Apps.js, Enclose the Top Component into Context Provider and pass it value to store.

   ![alt text](images/{C39F6A33-428D-436F-A547-B298760457CF}.png)

4. In the consuming Component, use the `useContext` Hook to fetch the context.

   ![alt text](images/{65BF76F7-A8D6-4538-9D66-36D15A086CFF}.png)

## Context API - Custom Hook

1. Define Custom Hook

   ![alt text](images/{A5350021-3F90-40F7-8BE5-1E3A101C9AC1}.png)

2. Use this Hook

   ![alt text](images/{3A27449B-E995-476A-A3AC-A013950FDE59}.png)

## Context API - Global Setup

![alt text](images/{FD0737BD-AEA1-4693-96E4-AD4DDCFC8BBF}.png)

```
{name,setName}

OR

{name: name, setName: setName}
```

![alt text](images/{6D906AC2-7986-4AF7-A03E-6AE5CF61FD4C}.png)

OR

![alt text]({02BECBAB-3440-4ACA-A38E-166041F7C1C2}.png)

`Steps regarding Global Context.`

1. Define the Global Context with Custom Hook

   ![alt text](images/{667EB367-6183-4B09-8CF6-262C349CF2EF}.png)

2. Apply the Global Context at Root Component

   ![alt text](images/{FD242C05-7FF9-4970-8C8C-2549C0DC381A}.png)

3. Use the Global Context within a Component to access state.

   ![alt text]({4F8BACEC-4D12-4577-B469-72704279C534}.png)

## What is the purpose of the GlobalContext.Provider in the Context API?

![alt text](images/{013604E1-74F6-43BD-BBE0-AFF3EE4097C2}.png)

To provide access to user state and setUser function to all nested components

## In the following UserContainer component, what will be displayed when user is null?

![alt text](images/{BF875137-CA49-4FB1-89DA-A4A9FB76CD83}.png)

Please login

## Why would you use a custom hook with the Context API?

![alt text](images/{D92DF1EE-BAE1-4FCC-B988-BFF93B86EAB5}.png)

To Simplify access to context value and avoid importing useContext repeatedly.

## useReducer Hook

Light version of Redux (State Mananagement Library)

As your project grows, it becomes difficult to manage state, as multiple developers work in your project.

But using Redux library also include much boiler point code.

React introduces useReducer to do the same job

`Problem Statement`

![alt text](images/{5C6AE63B-B607-4348-8867-DA5172686DD5}.png)

![alt text](images/{2F2E1B35-DF1D-4720-828D-5100C2DD6173}.png)

`Solution`

**Option 1**

![alt text](images/{66B1974F-BD7A-40E3-9B32-C06E6EED902E}.png)

![alt text](images/{6FB5B5B2-6A6A-4081-A9B9-99218D25E24E}.png)

**Option 2** - useReducer

1. Import `useReducer` from 'react'

   ![alt text](images/{C2E370CD-4B23-4702-8D6A-1D3C66BB94FF}.png)

2. Define default state

   ![alt text](images/{3EF260AF-6F4D-420B-B0C3-62CA52B59F45}.png)

3. Defind Reducer Function

   ![alt text](images/{A24EBA83-1DD3-49F3-A243-C0714C67B2AC}.png)

   ![alt text](images/{569C40DB-01A6-4D7A-9ACC-227C90DA6AED}.png)

   ![alt text]({031D3A50-BDD3-4B32-B39E-B667470DFE15}.png)

   ![alt text]({C2664AF8-E854-414B-B9B9-696E62B542A4}.png)

4. Define the `useReducer(reducer(),defaultState)` within the Top Component

   ![alt text](images/{97013053-2702-4961-9436-85B95907C25C}.png)

   ![alt text](images/{59EF31FD-6A9B-47BF-B600-CFD8B0BA4CB1}.png)

5. Use `state` object

   ![alt text]({7F233120-4A60-48F9-B3CE-F8CC427BCE99}.png)

you dispatch an action, and the action is handled in the reducer.

## useReducer - Actions and Default State

![alt text](images/{E77A4BDF-B331-467C-99A8-349402AD2575}.png)

![alt text](images/{C697E8EC-33ED-47A1-90E5-2989A0E2DB0C}.png)

OR

![alt text](images/{F9766985-CA61-44D4-A241-4F0DEDA60698}.png)

## useReducer - Reset

![alt text](images/{8A1760CA-62CB-4112-B51D-1D898A673FBE}.png)

![alt text](images/{BD252579-499A-4F93-B0B3-AA7663022750}.png)

## useReducer - Remove Item

![alt text](images/{0599DB64-7A14-4D60-9E12-D5A6FDD04B69}.png)

![alt text](images/{1DB92264-B689-4E34-9F83-41B721A135D4}.png)

![alt text](images/{22ED7847-DFB5-434D-8D4F-8A9FD8D2574E}.png)

## useReducer - Import/Export

- Create action.js and put all actions there and import/export

  ![alt text](images/{8C9DD756-3EC6-44EE-9BC3-8192C35ACACD}.png)

  ![alt text](images/{445A5DFF-F92A-4A9E-83EC-41A04E529BB1}.png)

- Create reducer.js and put the reducer there and import/export

  ![alt text](images/{81497862-EDA0-44EC-A922-6647F302E46D}.png)

  ![alt text](images/{53D5A1E5-ED2C-43A5-A73A-3906665D7443}.png)

## What is the purpose of the action.type in a reducer function?

Identifies the specific action to handle in the reducer function

## What is the role of dispatch in the useReducer hook?

```
const [state, dispatch] = useReducer(reducer, defaultState);
```

It triggers an action to update the state using the reducer logic

## Performance Intro

React is fast by default.

Components rerender, when state or props changes.

`Problem Statement`

Here the entire `List` rerenders everytime, the `count` is changed

![alt text]({C24086C8-722A-4EB6-8023-4F746E5438B7}.png)

`Root Cause`

When the parent element re-renders, even if the component's state or props have not changed.

`Fix`

1. useEffect() : will be invoked on first component render, not on rerender

   ![alt text](images/{4349BC37-9381-498E-BF7F-CAB7C324D43F}.png)

2. Encapsulate the state within the component.

   ![alt text](images/{0AAE6289-6CAB-46B4-A0B1-28FBF51E95B9}.png)

   `Validate by - Profiler`
   - Recoerd
     ![alt text](images/{DE3C1A78-D261-455A-80C6-A1CFAB9FB8A3}.png)

   - Validate
     ![alt text](images/{6D30C275-700E-44B8-B8FD-726B1F4264C5}.png)

## Axios

A promise based HTTP Client for browser and node.js

## Install Axios

```
npm install axios
```

## GET Request

![alt text](images/{49B2E920-9913-40E0-870F-A6A0C7F4727A}.png)

![alt text](images/{B2C86B2F-FCA2-492E-A72E-A671DD12B8BA}.png)

![alt text](images/{DAB11EE7-BE64-4A03-B4FB-C869A53BFBAB}.png)

![alt text](images/{B88E8B60-9F48-4381-8B02-6111896B5BD9}.png)

![alt text](images/{DBBA04FF-2B84-48AB-9A99-DF4E869A5A22}.png)

![alt text](images/{24967562-F69B-43BF-8CE3-7EB3B4A22037}.png)

Fetch API does not identity 4** 5** error code, the issue is handled by Axios

## Setup Header

![alt text](images/{A2790476-144A-4AEC-BB2E-B58BD00BA32C}.png)

## POST Request

![alt text](images/{CEB17446-05A3-41E7-A521-81F402800F63}.png)

![alt text](images/{993A9F82-26E7-48E4-977F-51A87C2602DB}.png)

## Global Axios Defaults

![alt text](images/{76A0721E-ED05-4017-82CC-12BEA6139643}.png)

![alt text](images/{36310F1E-8659-48C3-8021-63D25BFD1B3E}.png)

![alt text](images/{403EBA05-76DE-4F2B-8EC1-BEAC09F450A6}.png)

## Tailwind CSS

```
C:\Users\varin\varinder_workspace\vscode-workspace\all-in-all\learning\TailwindCSS\Readme.md
```

## React Cache

Manage and Cache Remote Data Fetching

## Setup Express Project

You can have a single vite react project with server application in that as well

`Steps`

1. Create a `server` folder parallel to `src`, add the index.ts file into that with express code
2. Update package.json with
   - Dependencies regarding express project

     ```
     npm install express cors morgan

     npm install -D @types/express @types/cors @types/morgan @types/node
     ```

3. Update the package.json for scripts

   ```
    "server": "ts-node server/index.ts"
   ```

4. If you want to run both Client/Server application together

   ```
   npm install -D concurrently

   "both": "concurrently \"npm run dev\" \"npm run server\"",
   ```

   ```
   npm run both
   ```

## Axios Custom Instance

`util.js`

![alt text](images/{5F14DA1A-E625-4344-9749-84661FC18A37}.png)

## HTTP Methods

- Define the type of actions that can be performed on the web server
-

![alt text](images/{DBA574FB-5980-40C5-B00A-C781E042EA5A}.png)

`CRUD Operations`

![alt text](images/{21B912B7-EA64-435D-B47A-01A10C66E69A}.png)

![alt text](images/{E0B8F922-BE7A-41F0-9284-481AD0F06285}.png)

![alt text](images/{10D61605-98A1-4F10-99DE-36C71A0F64FB}.png)

![alt text](images/{24E833FA-035A-4724-ABD2-8662CF9636D5}.png)

## API Docs

For each API you develop you need to provide the documentation regarding the usage of that applicaiton

- How to authenticate
- Different Endpoints with detailed description, input format, output format

## React Query

`Approach 1` : Using useEffect

![alt text](images/{0AC407BB-C3A8-47C9-A34B-1E7A9BBBAF64}.png)

Challenge : Keeping out app state in sync with the server.

`Approach 2`

![alt text](images/{69303868-2DDC-45DB-A822-50DA51C70D29}.png)

## React Query - Installation

```
npm install @tanstack/react-query
```

![alt text](images/{0828025E-7C09-4736-A4ED-4DE59B5A351C}.png)

In the latest version of React Query (V5), the 'isLoading' property has been replaced with 'isPending'.

A common rule of thumb:

- React Query → manages server state (data from APIs).
- Context API → manages client/UI state that needs to be shared across components.

If userData comes from an API, React Query already provides caching and sharing. Any component can access it, so no context API is needed.

## First Query

![alt text](images/{62F1B0EC-5652-47A5-AB42-0E17930693B1}.png)

![alt text](images/{930EB2B6-7B5A-4163-B172-B5AD918D6396}.png)

## Render Data

![alt text](images/{16D0059E-5D65-4ADB-8D3D-4E5F38C9C7F5}.png)

![alt text](images/{8C562C85-F160-4725-B106-97464A1FB712}.png)

To see, how the applicaiton loads in slow mode

![alt text](images/{F7C75B96-A16B-482E-804A-1AA370BD8A84}.png)

## Error Handling

![alt text](images/{D6C49028-4505-4239-A3E5-EF4C56D33FDA}.png)

`Backend API`

![alt text](images/{4D2171C4-B2FD-480C-ADB8-26466C59B209}.png)

![alt text](images/{EF7787CF-0CA7-4358-9F87-2DE7E61FBC7F}.png)

![alt text](images/{77EBACC7-FAB7-4D15-BE53-F2AC6720C723}.png)

![alt text](images/{2BB6022C-BF83-4DF6-BF4F-35DFBB18AA3E}.png)

![alt text](images/{0B3EC7E4-28A2-47C7-82AB-DCDE080BE3D2}.png)

## Thunder Client

VS Code Plugins for Testing API, like Postman

![alt text](images/{5785E3DA-ED63-4B24-AB81-E456788DB38A}.png)

## ADD - POST Request

- useQuery : Hook for GET
- useMutation : Hook for POST, PUT, DELETE

![alt text](images/{9EAD8BA0-F281-44AE-8DB1-56B4588991EB}.png)

![alt text](images/{F5860A82-1180-4E6A-8706-11D041BA5443}.png)

![alt text](images/{349FE9FA-2416-47E5-9E28-3B341AA76D96}.png)

![alt text](images/{8A2D34D4-9893-4CF9-BAE7-CCBA859349DF}.png)

## useMutation Helper

![alt text](images/{F9558DE3-344A-40A5-86EA-4A4D0DF43E50}.png)

For server side errors

![alt text](images/{8B98BB9A-7307-4FBD-AEE7-50BE61F99891}.png)

`Toast Effect`

![alt text](images/{D311EAC4-2B62-4A15-89A7-1914F5B422AD}.png)

![alt text](images/{78218C4B-A71F-4D7F-AC65-1F835E2B23C7}.png)

![alt text](images/{94D2F7DC-20E3-411B-817C-9F505300E1F4}.png)

![alt text](images/{F4D66344-2AD5-4663-A00E-4B5B080FD08B}.png)

So `invalidateQueries` keep the server state and client state in sync.
