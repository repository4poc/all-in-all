import React from 'react';
import { HeroCarousel } from './HeroCarousel';

export default function Hero() {
  return (
    <>
      <div className='flex lg:flex-row sm:flex-col lg:gap-50 sm:gap-15 items-center max-w-7xl'>
        <div>
          <h1 className='text-5xl md:text-6xl lg:text-7xl font-bold mb-6 leading-[1.1] tracking-tight opacity: 1; transform: none; dark: text-fuchsia-500;'>
            Varinder Gupta
            <br />
            <span className='text-transparent bg-clip-text bg-gradient-to-r  tracking-normal text-3xl from-emerald-400 to-blue-400'>
              Cloud Developer & DevOps Engineer
            </span>
          </h1>
          <div
            className='flex flex-wrap items-center gap-6 text-sm text-slate-400'
            style={{ opacity: '1', transform: 'none' }}
          >
            {['Cloud Developer', 'Platform Engineer', 'DevOps Engineer'].map(
              (skill) => {
                return (
                  <div className='flex items-center gap-2' key={skill}>
                    <svg
                      className='w-5 h-5 text-emerald-400'
                      fill='none'
                      viewBox='0 0 24 24'
                      stroke='currentColor'
                    >
                      <path
                        strokeLinecap='round'
                        strokeLinejoin='round'
                        strokeWidth='2'
                        d='M5 13l4 4L19 7'
                      ></path>
                    </svg>
                    <span>{skill}</span>
                  </div>
                );
              },
            )}
          </div>
          <div
            className='flex mb-8 mt-20 max-w-3xl items-center justify-center '
            style={{ opacity: '1', transform: 'none' }}
          >
            <a href='/demos'>
              <button className='group relative bg-emerald-600 hover:bg-emerald-500 text-white font-serif py-4 px-8 rounded-lg transition-all text-base shadow-lg shadow-emerald-500/25 hover:shadow-emerald-500/40 hover:scale-[1.02] active:scale-[0.98] '>
                Demos
                <span className='ml-2 inline-block group-hover:translate-x-1 transition-transform'>
                  →
                </span>
              </button>
            </a>
          </div>
        </div>
        <div>
          <img
            alt=''
            className='w-50 h-50 md:w-60 md:h-60 object-contain rounded-3xl'
            src='/Varinder_Photo.jpeg'
            style={{ transform: 'translateY(-0.966978px)' }}
          />
        </div>
      </div>
    </>
  );
}
