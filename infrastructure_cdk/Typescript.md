# Typescript

Typescript is a superset of Javascript.

Main features

- Types

Typescript Compiler (tsc) compies .ts files into .js

## How to use Typescript in a Nodejs Project

- npm init
- Add in the package.json
  - typescript as dev dependency and then run `npm install`
  - OR
  - npm i -D typescript

## ts-node :

- npm i -D ts-node
- npx ts-node example.ts

# Key files

- tsconfig.json

# Type Alisa

```
type Profession = 'Engineer' | 'Doctor'

type Employee = {
    name: string,
    age: number,
    married: boolean,
    profession?: Profession // make it optional
}

const employee1 : Employee = {
    name : 'varinder',
    age: 32,
    married: true,
    profession: 'Engineer'
}

const employee1 : Employee = {
    name : 'rajinder',
    age: 32,
    married: false
}
```

# Data Types

- string (not String)
- boolean (not bool)
- number (not integer)
- bigint
- object
- symbol
- null
- undefined
- any

```
let name: string = "Alice";
let age: number = 30;
let active: boolean = true;
let big: bigint = 123n;
let id: symbol = Symbol("id");
let empty: null = null;
let value: undefined = undefined;
```

# Types works inside functions as well

As tsc does not allow declartion without any datatype, so we have to expilictly specify type of function for parameters.

# Arrow function

```
function sayHello(msg: string): void {
  console.log(msg);
}

const sayHi = (a: string, b: string) => {
  console.log(a + b);
};

const messages = (...msgs: string[]): void => {
  msgs.forEach((msg) => {
    console.log(msg);
  });

  msg.push("hello")

  msg.pull(msgs[0])
};
```

# How to check type of a varible

```
typeof age -> number
typeof name -> string
```

# Any vs Undefined

| Type        | Meaning                                           |
| ----------- | ------------------------------------------------- |
| `any`       | “Can be anything”                                 |
| `undefined` | A specific value meaning “not assigned / missing” |
