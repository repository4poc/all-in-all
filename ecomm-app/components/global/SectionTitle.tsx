import React from 'react';
import { Separator } from '../ui/separator';

export default function SectionTitle({ text }: { text: string }) {
  return (
    <div>
      <h2 className='text-3xl font-medium justify-center capitalize mb-4'>
        {text}
      </h2>
      <Separator />
    </div>
  );
}
