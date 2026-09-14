import React from 'react';
import { Button } from '../ui/button';
import Link from 'next/link';
import { auth } from '@clerk/nextjs/server';

export default async function AdminButton() {
  const { userId } = await auth();

  const isAdminUser = userId === process.env.ADMIN_USER_ID;

  console.log(`isAdminUser:  ${isAdminUser}`);

  return (
    <>
      <Button
        variant='outline'
        className={`relative ${isAdminUser ? 'block' : 'hidden'}`}
      >
        <Link href='/admin/sales' className='font-serif w-full '>
          Admin
        </Link>
      </Button>
    </>
  );
}
