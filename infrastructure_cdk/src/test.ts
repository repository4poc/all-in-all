type Profession = "Engineer" | "Doctor";

type Employee = {
  name: string;
  age: number;
  married: boolean;
  profession?: Profession; // make it optional (Undefied)
};

const employee1: Employee = {
  name: "varinder",
  age: 32,
  married: true,
  profession: "Engineer",
};

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
};

let msgs: string[] = ["Hello", "Hi", "Welcome"];

msgs.forEach((m) => {
  console.log(m);
});

msgs.push("Howdy");

msgs.length;

let index = msgs.findIndex((m) => m === "Hi");
