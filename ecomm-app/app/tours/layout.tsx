const Layout = ({ children }: { children: React.ReactNode }) => {
  return (
    <div>
      <header className='bg-slate-500 py-2 w-1/2'>
        <h1>Nested Layout</h1>
      </header>
      {children}
    </div>
  );
};

export default Layout;
