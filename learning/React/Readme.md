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
- src
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
