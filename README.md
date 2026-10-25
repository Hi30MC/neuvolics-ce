# Neuvolics

> [!CAUTION]
> This is the CE port of Neuvolics. If you wish to view the PV version, see [here](https://github.com/ErikHaag/Neuvolics).
> Note that this port is done by Hi30, and is hosting on this page. All of the previous credits are still in order, go check them out!

## Introduction 

A new family of elements that has intriguing properties and transmutations.  
Will *you* take it for a spin?

## Credits
<pre>
   Charlie : Concept & SFX
Greenfield : Programming & Animations
      Hi30 : Glyph Textures + Port to CE
  Giacinto : Atom Textures
</pre>

This is the most collabrative project I've ever been a part of, And I must thank everyone here and on the Zachtronics discord sever more broadly for their help and ideas.

\- Greenfield.

# Documentation

The **neumetals** are a family of five metals that are isomorphic to [the cyclic group C₅](https://en.wikipedia.org/wiki/Cyclic_group). The **volics** can be seen as the three operations of the unit rotation, its inverse, and the identity rotation (do-nothing action). All of the atoms have mass one, except for Zephiron which has mass two. Not all glyphs preserve mass, as Putrefaction is lossy.

## Atoms

| Neumetals |  Volics  |
|:---------:|:--------:|
|  Hestium  |  Frixon  |
|   Azulum  |  Gelaron |
|   Taceum  | Zephiron |
|   Mitrum  |          |
|  Iridium  |          |

## Neumetallic Cycle

The neumetals form a closed cycle as described below:

Hestium -> Azulum -> Taceum -> Mitrum -> Iridium -> Hestium -> ...

where frixon translates forward (or clockwise) and gelaron translates backwards (anti-clockwise)

## Glyphs

- **Consolidation** : Joins one frixon and one gelaron and outputs one zephiron
- **Separation** : Splits one zephiron and spits out a frixon-gelaron pair.
- **Putrefaction** : Accepts up to two frixon or gelaron and cycles the metal in the bowl according to the cycle.
- **Fixation** : Accepts a neumetal and two of frixon or gelaron to cycle the metal once; returns a zephiron and the metal.
- **Cataclysm** : Acts as a zephiron sump; starts with three zephiron in storage. Furthermore, transmutes a zephiron and frixon/gelaron and swaps the position of the two, flipping the polarity of the non-zephiron volic.

## Other mods

This mod interfaces with many other mods, namely:

### Sennmetals

Fixation now takes a pair of zephiron and flips Azulum, Taceum, Mitrum, Iridium, and Hestium into their Sennmetallic forms.

### Origami

Translation between Azulum, Taceum, Mitrum, Iridium, Hestium, and Zephiron into their corresponding Orimetallic forms. 
