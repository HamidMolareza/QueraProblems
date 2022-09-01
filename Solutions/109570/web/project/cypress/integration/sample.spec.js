describe('snapp-shop sample tests:', () => {
  beforeEach(() => {
    // Load the page
    cy.visit('index.html');
  });

  it('check for border-radius property', () => {
    cy.get('img').should('have.css', 'border-radius').and('eq', '8px');
  });

  it('check for images alt', () => {
    cy.get('[alt="book"]').should('be.visible');
    cy.get('[alt="cosmetic"]').should('be.visible');
    cy.get('[alt="fashion"]').should('be.visible');
    cy.get('[alt="digital"]').should('be.visible');
  });

  it('check for flex property', () => {
    cy.get('.banners').should('have.css', 'flex-wrap').and('eq', 'wrap');
  });
});
