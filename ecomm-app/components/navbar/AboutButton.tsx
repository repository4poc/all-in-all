import React from 'react';
import { Button } from '../ui/button';
import Link from 'next/link';
import { FcAbout } from 'react-icons/fc';

export default function AboutButton() {
  return (
    <div>
      <Button variant='outline' className='relative'>
        <Link href='/about' className='font-serif w-full'>
          About
        </Link>
      </Button>
    </div>
  );
}
