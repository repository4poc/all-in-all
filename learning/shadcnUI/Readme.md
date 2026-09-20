## Shadcn/UI

- A set of pre-designed components and a code distribution platform
- Works with your favorite frameworks and AI models.
- Open Source
- Open Code.

|                        | **shadcn/ui**                     | **Bootstrap**           |
| ---------------------- | --------------------------------- | ----------------------- |
| Approach               | Copy components into your project | Install a UI framework  |
| Styling                | Tailwind CSS                      | Bootstrap CSS           |
| Customization          | ⭐⭐⭐⭐⭐                        | ⭐⭐⭐                  |
| Modern UI              | ⭐⭐⭐⭐⭐                        | ⭐⭐⭐                  |
| Speed to build         | ⭐⭐⭐⭐                          | ⭐⭐⭐⭐⭐              |
| React/Next.js          | **Excellent**                     | Good                    |
| Accessibility          | Very good                         | Good                    |
| Components             | Good, composable                  | **Huge library**        |
| Design freedom         | **Very high**                     | Moderate                |
| Bundle/control         | **Excellent**                     | More framework overhead |
| Learning curve         | Moderate                          | **Easy**                |
| Admin dashboards       | Excellent                         | **Excellent**           |
| Highly branded product | **Winner**                        | Can feel generic        |
| Legacy projects        | —                                 | **Winner**              |

## Installation

`For Exiting NextJS Project`

```
npx shadcn@latest init

```

https://ui.shadcn.com/docs/installation/next

The above command creates a `button.tsx` inside `components/ui` folder and `components.json` file

## Add Button component in your project

```
npx shadcn@latest add button/<component>
```

## How to use components

```
import { Button } from '@/components/ui/button';

export default function HomePage() {
  return (
    <div>
      <Button>Hello</Button>
    </div>
  );
}

```

## Component Props

![alt text](images/{2BCCD36D-C242-4E3F-8D7E-83B8EB24B646}.png)

```
import { Button } from '@/components/ui/button';

export default function HomePage() {
  return (
    <div>
      <Button variant='default' size='sm'>
        default
      </Button>
      <Button variant='outline' size='sm'>
        outline
      </Button>
      <Button variant='ghost' size='sm'>
        ghost
      </Button>
      <Button variant='destructive' size='sm'>
        destructive
      </Button>
      <Button variant='secondary' size='sm'>
        secondary
      </Button>
      <Button variant='link' size='sm'>
        link
      </Button>
    </div>
  );
}

```

![alt text](images/{55A3E0CF-B2B5-4825-A037-6DDC936E9BF0}.png)

## Custom Theme

https://ui.shadcn.com/create?utm_source=chatgpt.com&preset=bcivVKXQ

![alt text](images/{A38DAB5F-1899-4BFD-951C-7EAF84E5EF54}.png)

![alt text](images/{697458FE-B492-4EBF-B4BB-19C1B32969C3}.png)

Get the .root and put it into the `global.css`

:root and .dark only

![alt text](images/{369C01E9-E317-4563-B47F-A71E3BABB72F}.png)

## Dark Mode

```
npx shadcn@latest add dropdown-menu
```

https://ui.shadcn.com/docs/dark-mode/next

Note in latest Shaden/UI, instead of `asChild`, `render` is used

```
<DropdownMenuTrigger
        render={
          <Button variant='outline' size='icon'>
            <Sun className='h-[1.2rem] w-[1.2rem] scale-100 rotate-0 transition-all dark:scale-0 dark:-rotate-90' />
            <Moon className='absolute h-[1.2rem] w-[1.2rem] scale-0 rotate-90 transition-all dark:scale-100 dark:rotate-0' />
            <span className='sr-only'>Toggle theme</span>
          </Button>
        }
      ></DropdownMenuTrigger>
```
