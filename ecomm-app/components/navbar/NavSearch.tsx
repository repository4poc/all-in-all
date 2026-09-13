'use client';

import { Input } from '@/components/ui/input';
import { usePathname, useRouter, useSearchParams } from 'next/navigation';

export default function NavSearch() {
  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();

  const value = searchParams.get('product') ?? '';

  const handleSearch = (value: string) => {
    const params = new URLSearchParams(searchParams.toString());

    if (value) {
      params.set('product', value);
    } else {
      params.delete('product');
    }

    router.replace(`${pathname}?${params.toString()}`);
  };

  return (
    <Input
      placeholder='Search product...'
      type='search'
      className='max-w-xs mt-4 text-white'
      value={value}
      onChange={(e) => handleSearch(e.target.value)}
      hidden={!pathname.endsWith('/demos/ecommerce')}
    />
  );
}
