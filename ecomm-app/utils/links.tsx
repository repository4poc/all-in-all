type NavLink = {
  href: string;
  label: string;
};

export const links: NavLink[] = [
  { href: '/', label: 'home' },
  { href: '/about', label: 'about' },
  { href: '/demos', label: 'demos' },
  { href: '/cart', label: 'cart' },
  { href: '/orders', label: 'orders' },
];
