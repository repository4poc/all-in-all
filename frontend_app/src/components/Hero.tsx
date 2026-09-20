import { FaGithubSquare, FaLinkedin, FaTwitterSquare } from 'react-icons/fa';
import heroImg from '../assets/about.svg';

function Hero() {
  return (
    <div className='bg-emerald-100 py-10'>
      <div className='px-8 grid sm:grid-cols-2 items-center'>
        <article>
          <p className='text-3xl font-bold'>I'm Varinder Gupta</p>
          <p className='mt-4'>Full Stack Developer</p>
          <div className='mt-2 flex gap-x-2'>
            <a href='#'>
              <FaGithubSquare className='text-slate-500'></FaGithubSquare>
            </a>
            <a href='#'>
              <FaLinkedin className='text-slate-500'></FaLinkedin>
            </a>
            <a href='#'>
              <FaTwitterSquare className='text-slate-500'></FaTwitterSquare>
            </a>
          </div>
        </article>
        <article className='hidden sm:block'>
          <img src={heroImg} className='h-50 w-60 lg:h-96'></img>
        </article>
      </div>
    </div>
  );
}

export default Hero;
