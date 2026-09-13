import Container from '../global/Container';
import Logo from './Logo';
import NavSearch from './NavSearch';
import CartButton from './CartButton';
import { LogOut } from 'lucide-react';
import { DarkMode } from './DarkMode';
import { LinksDropDown } from './LinksDropDown';
import { Suspense } from 'react';
import { FcAbout } from 'react-icons/fc';
import AboutLink from './AboutLink';

export default function Navbar() {
  return (
    <Suspense fallback={<div>Loading...</div>}>
      <nav className='bg-cyan-950 dark:bg-muted max-w-auto py-4'>
        <Container className='flex flex-row justify-between items-center'>
          <Logo />
          <NavSearch />
          <div className='flex items-center gap-4'>
            <AboutLink />
            <CartButton />
            <DarkMode />
            <LinksDropDown />
          </div>
        </Container>
      </nav>
    </Suspense>
  );
}
