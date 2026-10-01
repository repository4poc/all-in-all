'use client';

import UserProfile from '@/components/demos/github/UserProfile';
import { useState } from 'react';

const AboutPage = () => {
  const [userName, setUserName] = useState('repository4poc');

  return (
    <section>
      <h1 className='flex flex-wrap gap-2 sm:gap-x-6 items-center justify-center text-4xl font-bold leading-none tracking-wide sm:text-6xl'>
        Varinder Gupta
      </h1>
      <h2 className='mt-6 text-lg tracking-wide leading-8 max-w-3xl mx-auto text-muted-foreground text-center mb-2'>
        Full Stack Developer | DevOps/Platform Engineer | AI Engineer
      </h2>
      <UserProfile userName={userName} />
      <section className='rounded-lg border border-amber-200 bg-amber-50 p-4 mt-2'>
        <p className='leading-relaxed text-slate-700'>
          I’m a Principal Engineer and Cloud and DevOps professional with 15+
          years of experience designing, building, and modernizing enterprise
          software, cloud platforms, and DevOps capabilities across Azure, AWS,
          Kubernetes-based environments.
        </p>
      </section>
      <section className='rounded-lg border border-amber-200 bg-amber-50 p-4 mt-2'>
        <p className='leading-relaxed text-slate-700'>
          My expertise spans software engineering, cloud architecture, platform
          engineering, DevOps, SRE, infrastructure automation, and cloud-native
          development. I work across the engineering lifecycle—from enterprise
          applications and APIs to scalable cloud platforms, Kubernetes, CI/CD,
          Infrastructure as Code, observability, developer self-service, and
          DevSecOps.
        </p>
      </section>
      <section className='rounded-lg border border-amber-200 bg-amber-50 p-4 mt-4'>
        <h3 className='mb-2 text-lg font-semibold text-amber-800'>
          Core expertise:
        </h3>
        <ul className='list-disc space-y-2 pl-6 leading-relaxed text-slate-700'>
          <li>Azure &amp; AWS Cloud Engineering</li>
          <li>Platform Engineering &amp; Developer Platforms</li>
          <li>DevOps &amp; Site Reliability Engineering (SRE)</li>
          <li>Kubernetes — AKS &amp; EKS</li>
          <li>Infrastructure as Code — Terraform &amp; Bicep</li>
          <li>GitOps — Argo CD &amp; Flux CD</li>
          <li>CI/CD — Azure DevOps, GitHub Actions &amp; Jenkins</li>
          <li>Cloud Automation &amp; DevSecOps</li>
          <li>Observability &amp; Production Engineering</li>
          <li>Cloud Migration &amp; Modernization</li>
          <li>.NET, Java &amp; Python</li>
          <li>API &amp; Integration Platforms</li>
        </ul>
      </section>
      <section className='rounded-lg border border-amber-200 bg-amber-50 p-4 mt-4'>
        <h3 className='mb-2 text-lg font-semibold text-amber-800'>
          Certifications
        </h3>
        <ul className='list-disc space-y-2 pl-6 leading-relaxed text-slate-700'>
          <li>Microsoft Certified: Azure Solutions Architect Expert</li>
          <li>Microsoft Certified: DevOps Engineer Expert</li>
          <li>Microsoft Certified: Azure Administrator Associate</li>
          <li>Microsoft Certified: Azure Developer Associate</li>
          <li>Microsoft Certified: Azure AI Engineer Associate</li>
          <li>AWS Certified Solutions Architect – Associate</li>
          <li>HashiCorp Certified: Terraform Associate</li>
        </ul>
      </section>
    </section>
  );
};

export default AboutPage;
