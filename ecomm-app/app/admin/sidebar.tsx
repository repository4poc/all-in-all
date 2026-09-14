'use client';
import { Button } from '@/components/ui/button';
import { adminLinks } from '@/utils/links';
import Link from 'next/link';
import { usePathname } from 'next/navigation';

export default function Sidebar() {
  const pathname = usePathname();

  return (
    <aside>
      {adminLinks.map((link) => {
        const isActivePage = pathname === link.href;
        const variant = isActivePage ? 'destructive' : 'ghost';
        return (
          <Button
            key={link.href}
            className='w-full capitalize'
            variant={variant}
          >
            <Link href={link.href} className='w-full text-left'>
              {link.label}
            </Link>
          </Button>
        );
      })}
    </aside>
  );
}
