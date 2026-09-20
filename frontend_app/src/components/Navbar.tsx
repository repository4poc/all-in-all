import { Links } from '../data';

const Navbar = () => {
  return (
    <nav className='bg-emerald-100'>
      <div className='mx-auto max-w-7xl px-8 py-4 flex flex-col  sm:flex-row sm:gap-x-16 '>
        <h2 className='font-bold text-'>
          Web <span className='text-emerald-600'>Dev</span>
        </h2>
        <div className='flex gap-x-3'>
          {Links.map((link) => {
            return (
              <div>
                <a href={link.href}>{link.text}</a>
              </div>
            );
          })}
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
