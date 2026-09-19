export type Demo = {
  id: number;
  title: string;
  description: string;
  href: string;
  techstack?: string;
};

export const demos = [
  {
    id: 1,
    title: 'E-commerce Application',
    description: 'An E-Comm Application with shoping cart and payment facility',
    techstack:
      'Next.js, React, TypeScript, Prisma, Supabase, Node.js, Tailwind CSS, Clerk, PostgreSQL, shadcn/ui, Vercel, Faker Library, Zod Library',
    href: '/demos/ecommerce',
  },
  {
    id: 2,
    title: 'Global Compliance Alignment',
    description:
      'Aligned with ESRS, GRI, BRSR, and TCFD frameworks for universal ESG compatibility.',
    href: '/demos/ecommerce',
  },
  {
    id: 3,
    title: 'Sustainability Mapping',
    description:
      ' Map your ESG topics to SDGs and international frameworks for transparent impact.',
    href: '/demos/ecommerce',
  },
  {
    id: 4,
    title: 'Impactful Dashboards',
    description:
      'Real-time ESG and carbon intelligence dashboards for strategic decisions.',
    href: '/demos/ecommerce',
  },
];
