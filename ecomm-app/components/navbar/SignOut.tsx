'use client';

import { SignOutButton } from '@clerk/nextjs';
import Link from 'next/link';
import { toast } from '../ui/toast';

export default function SignOut() {
  const onClickHanlder = () => {
    toast.add({
      type: 'success',
      description: 'Logout Successful.',
    });
  };

  return (
    <SignOutButton>
      <Link href='/' onClick={onClickHanlder}>
        Logout
      </Link>
    </SignOutButton>
  );
}
