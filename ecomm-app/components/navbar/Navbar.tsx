import Container from '../global/Container';
import Logo from './Logo';
import NavSearch from './NavSearch';
import CartButton from './CartButton';
import { LogOut } from 'lucide-react';
import { DarkMode } from './DarkMode';
import { LinksDropDown } from './LinksDropDown';

export default function Navbar() {
  return (
    <nav className='bg-cyan-950 dark:bg-muted max-w-auto py-4'>
      <Container className='flex flex-row justify-between items-center'>
        <Logo />
        <NavSearch />
        <div className='flex items-center gap-4'>
          <CartButton />
          <DarkMode />
          <LinksDropDown />
        </div>
      </Container>
    </nav>
  );
}
