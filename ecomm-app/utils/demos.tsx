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
    title: 'Search GitHub User',
    description:
      ' Map your ESG topics to SDGs and international frameworks for transparent impact.',
    href: '/demos/github',
    techstack:
      'GraphQL, Apollo Client, Next.js, React, TypeScript, Tailwind CSS',
  },
  {
    id: 3,
    title: 'DataBricks and GenAI',
    description:
      'The project is based on Apache Spark and Build Custom Machine Learning, Deep Learning, GenAI models and RAG Chatbots.',
    techstack: 'PySpark, Medallion Architecture, ',
    href: '/demos/databricks',
  },
  {
    id: 4,
    title: 'Impactful Dashboards',
    description:
      'Real-time ESG and carbon intelligence dashboards for strategic decisions.',
    href: '/demos/ecommerce',
  },
];
