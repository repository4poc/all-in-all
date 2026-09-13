import React from 'react';
import { Button } from '../ui/button';
import Link from 'next/link';
import { FcAbout } from 'react-icons/fc';

export default function DemosButton() {
  return (
    <div>
      <Button variant='outline' className='relative'>
        <Link href='/demos' className='font-serif'>
          Demos
        </Link>
      </Button>
    </div>
  );
}
