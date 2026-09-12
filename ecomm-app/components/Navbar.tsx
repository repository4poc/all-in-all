'use client';

import Link from 'next/link';

const Navbar = () => {
  return (
    <nav className='py-4 flex gap-x-8 items-center'>
      <Link href='/'>Home</Link>
      <Link href='/about'>About</Link>
      <Link href='/actions'>Actions</Link>
      <Link href='/contact'>Contact</Link>
      <Link href='/counter'>Counter</Link>
      <Link href='/tours'>TourPage</Link>
    </nav>
  );
};

export default Navbar;
