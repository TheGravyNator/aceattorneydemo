# Plate
"The unwashed dishes."
-> Snack

=== Snack ===
# neutral CrossExamine
Maya: I was having a late night snack.
+ [Press]
    -> PressSnack
+ [PresentWrong]
    -> WrongEvidence
+ [Continue]
    -> AfterMeal

=== AfterMeal ===
# thinking CrossExamine
Maya: After the meal, I wanted to go to my room.
+ [Press]
    -> PressSnack
+ [PresentWrong]
    -> WrongEvidence
+ [Continue]
    -> Remembered

=== Remembered ===
# shocked CrossExamine
Maya: But then I remembered that I needed to wash my plate!
+ [Press]
    -> PressSnack
+ [PresentWrong]
    -> WrongEvidence
+ [Continue]
    -> Washing

=== Washing ====
# pose CrossExamine
Maya: So I rinsed my plate under the sink and put it back in the cupboard.
+ [Press]
    -> PressSnack
+ [PresentWrong]
    -> WrongEvidence
+ [Continue]
    -> CrossExaminationEnd
+ [PresentRight]
    -> RightEvidence

=== CrossExaminationEnd ===
# thinking
Phoenix: It doesn't sound like she's lying...
# thinking
Phoenix: ...But the evidence says something else.
# neutral
Phoenix: There must be a contradiction in there somewhere.
-> Snack

=== PressSnack ===
# thinking
Phoenix: Wasn't it almost bed time?

# neutral
Phoenix: Doesn't your metabolism slow down, making you gain more weight?

# shocked
Maya: Wait is that true?!

# neutral
Franziska: You would do better to refrain from badgering my witness, Phoenix Wright.

# shocked
Phoenix: What's with that menacing look?
-> Snack

=== WrongEvidence ===
# pose
Franziska: Foolish fools make foolishly foolish mistakes, Phoenix Wright.

# shocked
Phoenix: I guess that wasn't the right choice.
-> Snack

=== RightEvidence ===
# pose
Phoenix: Are you sure about that, Maya?

# shocked
Maya: W-what?

# shocked
Franziska: W-what are you insinuating, Phoenix Wright?
-> END
