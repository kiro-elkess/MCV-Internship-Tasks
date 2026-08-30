# PhotoFolio — Bootstrap 5 Rebuild

A photography portfolio page rebuilt using Bootstrap 5, based on the [PhotoFolio](https://bootstrapmade.com/demo/PhotoFolio/) template.

---

## Technologies Used

- **Bootstrap v5.3.3** — Grid, Navbar, Cards, Modal, Utilities
- **Font Awesome 6.4.0** — Icons
- **Google Fonts** — Poppins
- **Vanilla JS** — Lightbox modal, gallery filter, mobile nav

---

## Responsive Layout

| Screen | Grid | Behavior |
| :--- | :--- | :--- |
| Mobile `<768px` | `col-12` | 1 column, collapsed navbar |
| Tablet `≥768px` | `col-md-6` | 2 columns |
| Desktop `≥992px` | `col-lg-4` | 3 columns, full navbar |

---

## Features

- Hover overlay on gallery items with zoom and fade effect
- Category filter (All / Nature / Outdoors / Forest)
- Modal lightbox on image click
- Smooth scroll with fixed navbar offset
- Mobile nav auto-closes on link click

---

## Q&A

**1. What is Bootstrap?**
An open-source CSS framework for building responsive, mobile-first websites with ready-made components and utility classes.

**2. Why use Bootstrap instead of writing everything from scratch?**
It speeds up development, handles cross-browser consistency, and removes the need to write repetitive layout CSS.

**3. What is the Bootstrap grid system?**
A 12-column, Flexbox-based layout system that uses `container`, `row`, and `col-*` classes to create responsive layouts.

**4. What does `col-md-6` mean?**
The element takes up 6 of 12 columns (half the row) on medium screens and above. On smaller screens it stretches to full width.

**5. What happens with `col-12 col-md-6 col-lg-4`?**
- Mobile: 1 item per row (full width)
- Tablet: 2 items per row
- Desktop: 3 items per row

**6. What is the difference between `container` and `container-fluid`?**
`container` has a max-width that adjusts per breakpoint. `container-fluid` is always 100% wide.

**7. What are Bootstrap breakpoints?**
Fixed screen-width thresholds (`sm` ≥576px, `md` ≥768px, `lg` ≥992px, `xl` ≥1200px) where the layout can shift.

**8. How does Bootstrap help make a website responsive?**
Through fluid grids, responsive utility classes like `d-none d-lg-block`, and `img-fluid` for scalable images.

**9. What does `d-flex` do?**
Sets `display: flex` on an element, enabling Flexbox layout for its children.

**10. What does `justify-content-between` do?**
Distributes children along the main axis with equal space between them and no space at the edges.

**11. When should you use Bootstrap utilities instead of custom CSS?**
For common tasks like spacing, alignment, color, and display — anything Bootstrap already covers well.

**12. Why avoid unnecessary custom CSS?**
It keeps the stylesheet small, avoids specificity conflicts, and makes the code easier to maintain.
