let lenis;

function initLenis() {
    if (typeof Lenis === 'undefined') return;

    lenis = new Lenis({
        duration: 1.6,
        easing: (t) => Math.min(1, 1.001 - Math.pow(2, -10 * t)),
        orientation: 'vertical',
        smoothWheel: true,
        wheelMultiplier: 0.85,
    });

    if (typeof gsap !== 'undefined' && typeof ScrollTrigger !== 'undefined') {
        lenis.on('scroll', ScrollTrigger.update);
        gsap.ticker.add((time) => lenis.raf(time * 1000));
        gsap.ticker.lagSmoothing(0);
    } else {
        function raf(time) { lenis.raf(time); requestAnimationFrame(raf); }
        requestAnimationFrame(raf);
    }
}

function initNavScroll() {
    const nav = document.getElementById('main-nav');
    if (!nav) return;
    window.addEventListener('scroll', () => {
        nav.classList.toggle('scrolled', window.scrollY > 60);
    }, { passive: true });
}

function initHeroAnimations() {
    if (typeof gsap === 'undefined') return;
    gsap.registerPlugin(ScrollTrigger);

    const words = document.querySelectorAll('.hero-word');
    if (words.length) {
        gsap.fromTo(words,
            { yPercent: 110, opacity: 0 },
            { yPercent: 0, opacity: 1, duration: 1.5, stagger: 0.13, ease: 'power4.out', delay: 0.15 }
        );
    }

    const subtitle = document.querySelector('.hero-sub');
    if (subtitle) {
        gsap.fromTo(subtitle,
            { opacity: 0, y: 24 },
            { opacity: 1, y: 0, duration: 1.1, ease: 'power3.out', delay: 0.75 }
        );
    }

    document.querySelectorAll('.reveal').forEach((el) => {
        gsap.fromTo(el,
            { opacity: 0, y: 38 },
            {
                opacity: 1, y: 0, duration: 1.1, ease: 'power3.out',
                scrollTrigger: { trigger: el, start: 'top 87%' }
            }
        );
    });

    const grid = document.querySelector('.products-irregular');
    if (grid) {
        gsap.fromTo('.product-item',
            { opacity: 0, y: 55 },
            {
                opacity: 1, y: 0, duration: 0.95, stagger: 0.07, ease: 'power3.out',
                scrollTrigger: { trigger: grid, start: 'top 82%' }
            }
        );
    }
}

function initDetailAnimations() {
    window.scrollTo({ top: 0, behavior: 'instant' });
    if (lenis) lenis.scrollTo(0, { immediate: true });
    if (typeof gsap === 'undefined') return;
    gsap.registerPlugin(ScrollTrigger);

    const els = document.querySelectorAll('.detail-animate');
    if (els.length) {
        gsap.fromTo(els,
            { opacity: 0, x: 28 },
            { opacity: 1, x: 0, duration: 0.9, stagger: 0.09, ease: 'power3.out', delay: 0.25 }
        );
    }
}

window.archetipo = {
    init: () => { initLenis(); initNavScroll(); },
    initHome: () => { initHeroAnimations(); },
    initDetail: () => { initDetailAnimations(); },
    initNav: () => { initNavScroll(); },
};

document.addEventListener('DOMContentLoaded', () => window.archetipo.init());
