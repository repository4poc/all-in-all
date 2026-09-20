'use client';

import SearchForm from '@/components/demos/github/SearchForm';
import UserProfile from '@/components/demos/github/UserProfile';
import { useState } from 'react';

export default function page() {
  const [userName, setUserName] = useState('repository4poc');
  return (
    <div>
      <SearchForm userName={userName} setUserName={setUserName} />
      <UserProfile userName={userName} />
    </div>
  );
}
