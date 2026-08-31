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
