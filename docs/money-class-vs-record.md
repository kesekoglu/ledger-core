# Money: class, not record

**Decision:** Money stays a class with hand-written equality.

**Why:** The constructor already checks the rules and we have 28 tests to prove
that equality and validation behaviours work as intended, so rewriting it as a
record would be risky.

**Risk of record:** A `with` expression copies the object without running the
constructor, so we would risk creating a Money with a negative amount
(e.g. `money with { Amount = -100 }`).

**If we started from scratch:** We would use a record with get-only properties.