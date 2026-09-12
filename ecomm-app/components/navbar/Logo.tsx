import { Button } from '../ui/button';
import Link from 'next/link';
import { FaPrescriptionBottle } from 'react-icons/fa';

export default function Logo() {
  return (
    <div className='flex items-center gap-4'>
      <a href='/' className='flex items-center gap-3 group'>
        <img
          alt='CarbonOps Logo'
          className='h-10 w-10 md:h-12 md:w-12 drop-shadow-lg transition-transform group-hover:scale-105'
          src='/my_logo.png'
        />
        <div className='flex flex-col'>
          <span className='text-2xl md:text-3xl font-bold tracking-tight text-emerald-300 leading-tight font-sans'>
            Varinder Gupta
          </span>
          <span className='text-xs md:text-sm font-medium text-slate-300 tracking-wide'>
            Digital Profile
          </span>
        </div>
      </a>
    </div>
  );
}
