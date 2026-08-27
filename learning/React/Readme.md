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

We can have multiple React Components within a file, but only one can be exported.

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

- key prop is very important,while rendering a list of items in React, because React uses the key prop to uniquely identify a list item and update only the changed element during rerendering (Optimized updates and rendering)

- Avoid using `key={index}`, rather use `key={book.id}` as using index as a key can cause issue, when reorder or modify the items, as the React rely on unique keys for stable updates.

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

![alt text]({8514032F-3074-4087-A4EA-FF41AF9363B8}.png)

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

![alt text](images/{4438B7EF-792E-44D9-9A6B-FFB4C52332A1}.png)

[For Arrys]

![alt text](images/{FAF3EBA8-FB8B-44EF-8829-FEF97CDF8037}.png)

[For Object]

![alt text](images/{3FAF5DF7-A667-4BFF-9685-FD774384A6B3}.png)
