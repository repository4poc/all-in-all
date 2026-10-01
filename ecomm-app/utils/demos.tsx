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
    title: 'Azure Landing Zone - Hub-Spoke Architecture (Multi-Region)',
    description:
      'A production-ready Azure Landing Zone built with Terraform and a multi-region hub-and-spoke network architecture. It provides a governed foundation for deploying and managing cloud workloads across Azure regions..',
    techstack:
      'Microsoft Azure, Terraform, Azure Virtual Networks, Hub-and-Spoke Networking, VNet Peering, Azure Firewall, Azure Bastion, VPN Gateway / ExpressRoute, Network Security Groups, Azure Policy, Azure Resource Groups, ACR, AKS, Log Analytics Workspace, Virtual Machines',
    href: '/demos/landingzone',
  },
  {
    id: 3,
    title: 'DataBricks and GenAI',
    description:
      'The project is based on Apache Spark and Build Custom Machine Learning, Deep Learning, GenAI models and RAG Chatbots.',
    techstack: 'PySpark, Medallion Architecture, ',
    href: '/demos/databricks',
  },
];
