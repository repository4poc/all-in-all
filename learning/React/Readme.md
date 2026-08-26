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
- Use className instead of class (reserved for native HTML)

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
