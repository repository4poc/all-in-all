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
      <p className='mt-6 text-lg tracking-wide leading-8 max-w-3xl mx-auto text-muted-foreground'>
        Lead Software Engineer | Cloud-Native & AI Solutions with 15+ years of
        experienced in
      </p>
      <UserProfile userName={userName} />
    </section>
  );
};

export default AboutPage;
