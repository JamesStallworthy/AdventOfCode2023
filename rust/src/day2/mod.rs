struct Hand {
    blue: u16,
    red: u16,
    green: u16,
}

struct Game {
    hands: Vec<Hand>,
}

impl Game {
    fn is_game_possible(&self, max_red: u16, max_green: u16, max_blue: u16) -> bool {
        for hand in self.hands.iter() {
            if hand.red > max_red || hand.green > max_green || hand.blue > max_blue {
                return false;
            }
        }

        true
    }

    fn power(&self) -> u16 {
        let mut min_red: u16 = 0;
        let mut min_green: u16 = 0;
        let mut min_blue: u16 = 0;

        for hand in self.hands.iter() {
            if hand.red > min_red {
                min_red = hand.red;
            }
            if hand.green > min_green {
                min_green = hand.green;
            }
            if hand.blue > min_blue {
                min_blue = hand.blue;
            }
        }

        min_red * min_green * min_blue
    }
}

fn parse_hands(hands: &str) -> Vec<Hand> {
    let mut parsed_hands: Vec<Hand> = Vec::new();

    for hand in hands.split("; ") {
        parsed_hands.push(parse_hand(hand));
    }

    parsed_hands
}

fn parse_hand(hand: &str) -> Hand {
    let mut red: u16 = 0;
    let mut blue: u16 = 0;
    let mut green: u16 = 0;

    for ball in hand.split(", ") {
        if ball.ends_with("red") {
            red = get_number_from_ball(ball);
        } else if ball.ends_with("blue") {
            blue = get_number_from_ball(ball);
        } else if ball.ends_with("green") {
            green = get_number_from_ball(ball);
        }
    }

    Hand { red, blue, green }
}

fn get_number_from_ball(ball: &str) -> u16 {
    let split_ball: Vec<&str> = ball.split(" ").collect();

    let ball_number = split_ball[0];

    ball_number.parse::<u16>().unwrap()
}

fn parse_games(games: &Vec<String>) -> Vec<Game> {
    let mut game_structs: Vec<Game> = Vec::new();
    for game in games {
        let split_game: Vec<String> = game.split(": ").map(|s: &str| s.to_string()).collect();

        let raw_hands = &split_game[1];

        let parsed_hands = parse_hands(raw_hands);

        let new_game = Game {
            hands: parsed_hands,
        };

        game_structs.push(new_game);
    }

    game_structs
}

fn find_valid_games_sum(games: &Vec<String>, max_red: u16, max_green: u16, max_blue: u16) -> u32 {
    let games = parse_games(games);
    let mut sum: u32 = 0;

    for (i, game) in games.iter().enumerate() {
        if game.is_game_possible(max_red, max_green, max_blue) {
            sum += u32::try_from(i).unwrap() + 1;
        }
    }

    sum
}

fn find_games_power_sum(games: &Vec<String>) -> u32 {
    let games = parse_games(games);

    let mut sum: u32 = 0;

    for game in games.iter() {
        sum += game.power() as u32;
    }

    sum
}

pub fn solve() {
    let rows = crate::utils::split_into_rows_vec(crate::utils::load_input("./day2/input.txt"));

    println!("{}", find_valid_games_sum(&rows, 12, 13, 14));
    println!("{}", find_games_power_sum(&rows));
}

#[test]
fn test1() {
    assert_eq!(
        find_valid_games_sum(
            &vec!["Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green".to_string()],
            20,
            20,
            20
        ),
        1
    );
}

#[test]
fn test2() {
    assert_eq!(
        find_valid_games_sum(
            &vec!["Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green".to_string()],
            1,
            1,
            1
        ),
        0
    );
}

#[test]
fn test3() {
    assert_eq!(
        find_valid_games_sum(
            &vec!["Game 1: 4 red, 3 blue; 6 blue, 16 green; 9 blue, 13 green, 1 red; 10 green, 4 red, 6 blue".to_string()],
            12,
            13,
            14
        ),
        0
    );
}

#[test]
fn test4() {
    assert_eq!(
        find_valid_games_sum(
            &vec!["Game 2: 2 green, 3 blue; 11 red; 2 green, 5 red, 1 blue".to_string()],
            12,
            13,
            14
        ),
        1
    );
}

#[test]
fn test5() {
    assert_eq!(
        find_games_power_sum(&vec![
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green".to_string()
        ]),
        48
    );
}
