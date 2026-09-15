import { Separator } from '@/components/ui/separator';
import React, { Children } from 'react';
import Sidebar from './sidebar';

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <>
      <h2 className='text-2xl pl-8'>Dashboard</h2>
      <Separator className='mt-2' />
      <div className='mt-12 grid grid-cols-12'>
        <div className='lg:col-span-2 col-span-4'>
          <Sidebar />
        </div>

        <div className='lg:col-span-10 col-span-8'>{children}</div>
      </div>
    </>
  );
}
