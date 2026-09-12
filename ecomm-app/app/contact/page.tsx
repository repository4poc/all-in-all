import Link from 'next/link';

const Contact = () => {
  return (
    <>
      <p className='text-3xl'>Contact page content</p>
      <Link href='/' className='text-sm text-blue-500 inline-block mt-6'>
        Contact page Content
      </Link>
    </>
  );
};

export default Contact;
