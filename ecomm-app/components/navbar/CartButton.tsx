'use client';

import Link from 'next/link';
import { Button } from '../ui/button';
import { LuShoppingCart } from 'react-icons/lu';
import { usePathname } from 'next/navigation';
function CartButton() {
  const pathname = usePathname();

  return (
    <Button
      variant='outline'
      size='icon'
      className='flex justify-center items-center relative'
      hidden={!pathname.endsWith('/demos/ecommerce')}
    >
      <Link href='/cart'>
        <LuShoppingCart />
        <span className='absolute -top-3 -right-3 bg-primary text-white rounded-full h-6 w-6 flex items-center justify-center text-xs'>
          {9}
        </span>
      </Link>
    </Button>
  );
}
export default CartButton;
