import Container from '../global/Container';
import Logo from './Logo';
import NavSearch from './NavSearch';
import CartButton from './CartButton';
import { LogOut } from 'lucide-react';
import { DarkMode } from './DarkMode';
import { LinksDropDown } from './LinksDropDown';
import { Suspense } from 'react';
import AboutButton from './AboutButton';
import DemosButton from './DemosButton';
import AdminButton from './AdminButton';

export default function Navbar() {
  return (
    <Suspense fallback={<div>Loading...</div>}>
      <nav className='bg-cyan-950 dark:bg-muted max-w-auto py-4'>
        <Container className='flex flex-col lg:flex-row justify-between items-center'>
          <Logo />
          <NavSearch />
          <div className='flex items-center gap-4 mt-4'>
            <AboutButton />
            <DemosButton />
            <AdminButton />
            <CartButton />
            <DarkMode />
            <LinksDropDown />
          </div>
        </Container>
      </nav>
    </Suspense>
  );
}
